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
                bool available = true;

                Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 dir = Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position);
                float angle = Mathf.Atan2(dir.y, dir.x);

                int x = worldPosition.x > 0 ? Mathf.RoundToInt(worldPosition.x) : -Mathf.RoundToInt(Mathf.Abs(worldPosition.x));
                int y = worldPosition.y > 0 ? Mathf.RoundToInt(worldPosition.y) : -Mathf.RoundToInt(Mathf.Abs(worldPosition.y));

                int pointX = (transform.position.x + math.cos(angle) * (Radius - 1)) > 0 ? Mathf.RoundToInt(transform.position.x + math.cos(angle) * (Radius - 1)) : -Mathf.RoundToInt(Mathf.Abs(transform.position.x + math.cos(angle) * (Radius - 1)));
                int pointY = (transform.position.y + math.sin(angle) * (Radius - 1)) > 0 ? Mathf.RoundToInt(transform.position.y + math.sin(angle) * (Radius - 1)) : -Mathf.RoundToInt(Mathf.Abs(transform.position.y + math.sin(angle) * (Radius - 1)));

                int vecX = x - Mathf.RoundToInt(transform.position.x);
                int vecY = y - Mathf.RoundToInt(transform.position.y);
                int vecLen = (int)math.sqrt(vecX * vecX + vecY * vecY);

                if (vecLen >= Radius) available = false;
                if (available && IsPlaceTaken(x, y)) available = false;

                if (available) FlyingBuilding.transform.position = new Vector2(x, y);
                else FlyingBuilding.transform.position = new Vector2(pointX, pointY);

                FlyingBuilding.SetTransparent(available);

                if (available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(x, y);
                }
                else if (!IsPlaceTaken(x, y) && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuilding(pointX, pointY);
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
            for (int x = -Radius; x <= Radius; x++)
            {
                for (int y = -Radius; y <= Radius; y++)
                {
                    if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                    else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                    Gizmos.DrawCube(new Vector3(Mathf.RoundToInt(transform.position.x + x),
                        Mathf.RoundToInt(transform.position.y + y), 0), new Vector3(1, 1, 0));
                }
            }

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                new Vector2(transform.position.x, transform.position.y),
                new Vector2(worldPosition.x, worldPosition.y));

            Gizmos.color = Color.green;
            Gizmos.DrawLine(
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(transform.position.x + math.cos(angle) * Radius,
                        transform.position.y + math.sin(angle) * Radius));

        }
    }
}