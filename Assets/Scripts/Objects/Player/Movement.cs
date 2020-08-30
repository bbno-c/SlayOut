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
        public AnimatorOverrider AnimatorOverrider;
        public Rigidbody2D PlayerRigidbody;

        public Transform TorsoTransform;
        public Transform LegsTransform;

        public float Speed;
        private MovementState _state;
        private float _timer;

        private Vector3 _moveDirection;
        private float _lookDirection;

        void Update()
        {
            if (_timer > 0f)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    if (_state == MovementState.Jump)
                    {
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
            TorsoTransform.rotation = Quaternion.AngleAxis(_lookDirection, Vector3.forward);

            if (_state != MovementState.Jump)
            {
                AnimatorOverrider.Animator.SetFloat("Magnitude", _moveDirection.magnitude);
                LegsTransform.rotation = Quaternion.AngleAxis((Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg), Vector3.forward);

                PlayerRigidbody.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * Speed;
            }
            else
                PlayerRigidbody.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * Speed * 2f;
        }

        public void MoveDirection(Vector3 direction)
        {
            _moveDirection = direction.normalized;
        }

        public void LookDirection(float direction)
        {
            _lookDirection = direction;
        }

        public void Jump()
        {
            if (_state == MovementState.Jump || _state == MovementState.DelayBetwenJumps)
                return;

            AnimatorOverrider.Animator.SetFloat("Magnitude", 0);

            MovementSetState(MovementState.Jump, 0.3f);
        }

        private void MovementSetState(MovementState state, float timer)
        {
            _state = state;
            _timer = timer;
        }
    }
}