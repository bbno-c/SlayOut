using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;

namespace Objects
{
    public enum MovementState
    {
        None,
        Jump,
        DelayBetwenJumps
    }

    public class Movement : MonoBehaviour
    {
        public Sprite LegsOnJumpSprite;
        public SpriteRenderer LegsSpriteRenderer;

        public AnimatorOverrider AnimatorOverrider;
        public Rigidbody2D PlayerRigidbody;
        public Transform TorsoTransform;
        public Transform LegsTransform;

        private Vector2 _jumpDir;

        public float Speed;
        private MovementState _state;
        private float _timer;


        private Vector3 _moveDirection;
        private float _lookDirection;

        void Update()
        {
            TorsoTransform.rotation = Quaternion.AngleAxis(_lookDirection, Vector3.forward);

            if(_state != MovementState.Jump)
            {
                AnimatorOverrider.Animator.SetFloat("Magnitude", _moveDirection.magnitude);
                LegsTransform.rotation = Quaternion.AngleAxis((Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg), Vector3.forward);
            }

            if (_timer > 0f)
            {
                if (_state == MovementState.Jump)
                    PlayerRigidbody.velocity = new Vector2(_jumpDir.x, _jumpDir.y) * Speed*2;

                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    if (_state == MovementState.Jump)
                    {
                        LegsSpriteRenderer.sprite = null;
                        MovementSetState(MovementState.DelayBetwenJumps, 1f);
                    }
                    else
                    {
                        MovementSetState(MovementState.None, 0f);
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (_state != MovementState.Jump)
                PlayerRigidbody.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * Speed;
        }

        public void MoveDirection(Vector3 direction)
        {
            _moveDirection = direction;
        }

        public void LookDirection(float direction)
        {
            _lookDirection = direction;
        }

        public void Jump()
        {
            if (_state == MovementState.Jump || _state == MovementState.DelayBetwenJumps)
                return;

            _jumpDir.x = _moveDirection.x;
            _jumpDir.y = _moveDirection.y;

            AnimatorOverrider.Animator.SetFloat("Magnitude", 0);

            LegsSpriteRenderer.sprite = LegsOnJumpSprite;

            MovementSetState(MovementState.Jump, 0.5f);
        }

        private void MovementSetState(MovementState state, float timer)
        {
            _state = state;
            _timer = timer;
        }
    }
}