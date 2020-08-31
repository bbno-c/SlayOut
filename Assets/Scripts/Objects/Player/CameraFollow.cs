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
        private float _angle;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        void Start()
        {
            offset.z = transform.position.z;
        }

        void LateUpdate()
        {
            if (Target != null)
            {
                //Vector3 dir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Target.position);
                //float angle = Mathf.Atan2(dir.y, dir.x);
                //float pointX = (Target.position.x + math.cos(_angle) * (Radius));
                //float pointY = (Target.position.y + math.sin(_angle) * (Radius));
                //offset.x = Radius;
                //offset.y = Radius;

                Vector3 desiredPosition = /*Target.position+*/ offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
                transform.position = smoothedPosition;
            }
        }

        public void CameraDirection(float x, float y)
        {
            offset.x = x;
            offset.y = y;
        }

        private void OnDrawGizmos()
        {
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPos = mainCamera.WorldToScreenPoint(transform.position);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                new Vector2(targetPos.x, targetPos.y),
                new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            //Gizmos.color = Color.green;
            //Gizmos.DrawLine(
            //new Vector2(transform.position.x, transform.position.y),
            //new Vector2(transform.position.x + math.cos(angle) * Radius,
            //            transform.position.y + math.sin(angle) * Radius));

        }
    }
}