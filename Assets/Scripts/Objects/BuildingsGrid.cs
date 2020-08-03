using System;
using UnityEngine;
using Unity.Mathematics;
using System.ComponentModel;

namespace Objects
{
    public class BuildingsGrid : MonoBehaviour
    {
        private Building FlyingBuilding;
        private Camera mainCamera;

        private int _radius;

        public int Radius { get => _radius; set => _radius = value; }

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        internal void Initialize()
        {

        }

        public void StartPlacingBuilding(Building buildingPrefab)
        {
            if (FlyingBuilding != null)
            {
                Destroy(FlyingBuilding.gameObject);
            }
            else
            {
                FlyingBuilding = Instantiate(buildingPrefab);
            }
        }

        private void Update()
        {
            if (FlyingBuilding != null)
            {
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.y);

                bool available = true;

                var dir = Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position);
                var angle = Mathf.Atan2(dir.y, dir.x);
                
                float pointX = transform.position.x + math.cos(angle) * Radius;
                float pointY = transform.position.y + math.sin(angle) * Radius;

                float lenghtX = math.abs(transform.position.x + math.cos(angle) * Radius) - math.abs(transform.position.x);
                float lenghtY = math.abs(transform.position.y + math.sin(angle) * Radius) - math.abs(transform.position.y);

                float CurlenghtX = math.abs(math.abs(worldPosition.x) - math.abs(transform.position.x));
                float CurlenghtY = math.abs(math.abs(worldPosition.y) - math.abs(transform.position.y));

                float length = math.sqrt(CurlenghtX * CurlenghtX + CurlenghtY * CurlenghtY);

                if (math.abs(length) > math.abs(Radius)) available = false;
                //if (math.abs(CurlenghtY) > math.abs(Radius)) available = false;

                if (available && IsPlaceTaken(x, y)) available = false;

                if (available) FlyingBuilding.transform.position = new Vector2(x, y);
                else FlyingBuilding.transform.position = new Vector2(Mathf.RoundToInt(pointX), Mathf.RoundToInt(pointY));

                FlyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x, y);
                }
            }
        }

        private bool IsPlaceTaken(int placeX, int placeY)
        {


            return false;
        }

        private void PlaceFlyingBuilding(int placeX, int placeY)
        {
            FlyingBuilding.SetNormal();
            FlyingBuilding = null;
        }

        private void OnDrawGizmos()
        {
            //for (int x = -Radius; x < Radius; x++)
            //{
            //    for (int y = -(Radius - Math.Abs(x)); y < (Radius - Math.Abs(x)); y++)
            //    {
            //        if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
            //        else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

            //        Gizmos.DrawCube(new Vector3(Mathf.RoundToInt(transform.position.x + x), Mathf.RoundToInt(transform.position.y + y), 0), new Vector3(1, 1, 0));
            //    }
            //}

            {
                Gizmos.color = Color.red;
                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.y);

                var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                var angle = Mathf.Atan2(dir.y, dir.x);

                Gizmos.DrawLine(
                    new Vector2(transform.position.x, transform.position.y),
                    new Vector2(x, y));

                Gizmos.color = Color.green;
                Gizmos.DrawLine(
                new Vector2(transform.position.x, transform.position.y),
                new Vector2(transform.position.x + math.cos(angle) * Radius,
                            transform.position.y + math.sin(angle) * Radius));
            }

        }
    }
}