using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;

namespace Objects
{
    public class Movement : MonoBehaviour
    {
        public AnimatorOverrider AnimatorOverrider;
        public Rigidbody2D PlayerRigidbody;
        public Transform TorsoTransform;
        public Transform LegsTransform;


        private Vector3 _moveDirection;
        private float _lookDirection;

        void Update()
        {
            AnimatorOverrider.Animator.SetFloat("Magnitude", _moveDirection.magnitude);

            TorsoTransform.rotation = Quaternion.AngleAxis(_lookDirection, Vector3.forward);
            LegsTransform.rotation = Quaternion.AngleAxis((Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg), Vector3.forward);
        }

        private void FixedUpdate()
        {
            PlayerRigidbody.velocity = new Vector2(_moveDirection.x, _moveDirection.y);
        }

        public void MoveDirection(Vector3 direction)
        {
            _moveDirection = direction;
        }

        public void LookDirection(float direction)
        {
            _lookDirection = direction;
        }
    }
}