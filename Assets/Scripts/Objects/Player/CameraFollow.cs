using UnityEngine;
using Unity.Mathematics;
using System.Diagnostics;
using Cinemachine;

namespace Objects
{
    public class CameraFollow : MonoBehaviour
    {
        public Character Character;
        public Transform Target { get; set; }
        public CinemachineVirtualCamera CinemachineVirtualCamera;
        public Camera mainCamera;

        public bool CodeActivate;
        public float Radius;
        public float smoothSpeed;

        public Vector2 mousePos;
        public Vector3 offset = new Vector3();
        public Vector3 mouseOffset;

        private void Awake()
        {
            mainCamera = Camera.main;

        }

        void Start()
        {
            offset.z = transform.position.z;
        }

        void FixedUpdate()
        {
            if (Target != null)
            {
                CinemachineVirtualCamera.Follow = Target;
            }

            if (Target != null && CodeActivate)
            {
                Vector3 dir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x);

                mousePos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                mousePos *= 2;
                mousePos -= Vector2.one;
                float max = 0.9f;
                if (Mathf.Abs(mousePos.x) > max || Mathf.Abs(mousePos.y) > max)
                {
                    mousePos = mousePos.normalized;
                }

                //float angle = Mathf.Atan2(mousePos.y, mousePos.x);
                float pointX = (Mathf.Cos(angle) * (Radius));
                float pointY = (Mathf.Sin(angle) * (Radius));
                offset.x = pointX;
                offset.y = pointY;

                mouseOffset = mousePos;

                mouseOffset *= Radius;
                mouseOffset.z = offset.z;

                Vector3 desiredPosition = Target.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;
            }
        }
    }
}