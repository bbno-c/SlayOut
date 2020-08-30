using UnityEngine;
using Unity.Mathematics;

namespace Objects
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Target { get; set; }
        public float Radius;
        private Vector3 offset = new Vector3();
        private Camera mainCamera;
        public float smoothSpeed;

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
                Vector3 dir = (Input.mousePosition) - mainCamera.WorldToScreenPoint(Target.position);
                float angle = Mathf.Atan2(dir.y, dir.x);
                float pointX = (Target.position.x + math.cos(angle) * (Radius ));
                float pointY = (Target.position.y + math.sin(angle) * (Radius ));
                offset.x = pointX;
                offset.y = pointY;
                //offset.x = Target.position.x >= 0 ? offset.x : -offset.x;
                //offset.y = Target.position.y >= 0 ? offset.y : -offset.y;transform.position = new Vector2(pointX, pointY);
                //transform.position = new Vector3(pointX, pointY, offset.z);
                
                Vector3 desiredPosition = /*Target.position +*/ offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;
            }
        }
    }
}