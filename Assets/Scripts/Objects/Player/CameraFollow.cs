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
        public float smoothSpeed;

        public Vector2 mousePos;
        public Vector3 offset = new Vector3();
        public Vector3 mouseOffset;
        //public Vector3 dir;

        public Vector3 curRotation;
        public float curAngle;
        public Vector3 prevRotation;
        public float prevAngle;
        public Vector3 equalToRot;
        public float equalToAngle;
        public float difAngle;
        public float rotAngle;
        public float dif;
        bool t;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        void Start()
        {
            t = true;
            CameraZ = transform.position.z;
            prevRotation = Vector3.zero;
            prevAngle = 0;
        }

        void FixedUpdate()
        {
            if (Target != null)
            {
                if (t)
                {
                    //prevRotation = Character.Movement.TorsoTransform.right;
                    t = !t;
                }

                curRotation = Character.Movement.TorsoTransform.right;
                curAngle = Mathf.Atan2(curRotation.y, curRotation.x) * Mathf.Rad2Deg;
                prevAngle = Mathf.Atan2(prevRotation.y, prevRotation.x) * Mathf.Rad2Deg;

                if (math.abs(curAngle-prevAngle) > 25f)
                {
                    dif = curAngle - prevAngle;
                    rotAngle = prevAngle + dif;
                    prevRotation = new Vector3(Mathf.Cos(rotAngle), Mathf.Sin(rotAngle),0);

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