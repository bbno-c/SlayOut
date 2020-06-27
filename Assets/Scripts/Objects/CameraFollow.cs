using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform Target { get; set; }
        private Vector3 offset = new Vector3();
        void Start()
        {
            offset.z = transform.position.z;
        }
        void Update()
        {
            if (Target != null)
            {
                transform.position = Target.position + offset;
            }
        }
    }
}