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
        public Camera mainCamera;

        public float CameraZ;
        public float Radius;
        public float Angle;
        public float smoothSpeed;

        public Vector3 offset = new Vector3();

        public Vector3 mouseDir;

        public Vector2 mousePos;
        public Vector3 mouseOffset;

        public Vector3 curRotation;
        private float curAngle;
        private float prevAngle;
        public Vector3 camDir;
        public Vector3 difRot;
        public float mouseAngle;
        public float camAngle;

        public float newCamAngle;
        private float xangle;
        public Vector3 xRot;
        private Vector3 prevRotation;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        void Start()
        {
            CameraZ = transform.position.z;
            camDir = Vector3.zero; prevRotation = Vector3.zero;
            camAngle = 0; prevAngle = 0;
        }

        void LateUpdate()
        {
            if (Target != null)
            {
                //mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Target.position);
                //mouseAngle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
                //camAngle = Mathf.Atan2(camDir.y, camDir.x) * Mathf.Rad2Deg;
                //if (mouseAngle > camAngle+25f)
                //{
                //    camAngle += mouseAngle - 25f;
                //    //camAngle *= Mathf.Deg2Rad;
                //    camDir = new Vector3(Mathf.Cos(camAngle* Mathf.Deg2Rad), Mathf.Sin(camAngle* Mathf.Deg2Rad),0);
                //    offset = camDir * Radius;
                //}
                curRotation = Character.Movement.TorsoTransform.right;

                curAngle = Mathf.Atan2(curRotation.y, curRotation.x) * Mathf.Rad2Deg;
                prevAngle = Mathf.Atan2(prevRotation.y, prevRotation.x) * Mathf.Rad2Deg;

                if (Mathf.Abs(curAngle - prevAngle) > Angle)
                {
                    xangle = prevAngle + (curAngle > prevAngle ? Angle : -Angle);
                    newCamAngle += curAngle - xangle;
                    xRot = new Vector3(Mathf.Cos(newCamAngle * Mathf.Deg2Rad), Mathf.Sin(newCamAngle * Mathf.Deg2Rad), 0);
                    //difRot = curRotation - xRot;
                    //difRot -= xRot;
                    prevRotation = xRot;

                    offset = prevRotation * Radius;
                }

                //dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Target.position);

                //mousePos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                //mousePos *= 2;
                //mousePos -= Vector2.one;
                //float max = 0.9f;
                //if (Mathf.Abs(mousePos.x) > max || Mathf.Abs(mousePos.y) > max)
                //{
                //    mousePos = mousePos.normalized;
                //}
                //mouseOffset = mousePos;
                //mouseOffset *= Radius;
                //mouseOffset.z = offset.z;

                offset.z = CameraZ;
                Vector3 desiredPosition = Target.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;
            }
        }
    }
}