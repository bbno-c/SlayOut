using System;
using UnityEngine;

namespace Objects
{
    public class BuildingsGrid : MonoBehaviour
    {
        private Building FlyingBuilding;
        private Camera mainCamera;

        private Transform _playerTransform;
        private float _radius;

        public float Radius { get => _radius; set => _radius = value; }

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        internal void Initialize()
        {
            throw new NotImplementedException();
        }

        public void ApplyAbility()
        {
            //StartPlacingBuilding(buildingPrefab);
        }

        public void StartPlacingBuilding(Building buildingPrefab)
        {
            if (FlyingBuilding != null)
            {
                Destroy(FlyingBuilding.gameObject);
            }
            
            FlyingBuilding = Instantiate(buildingPrefab);
        }

        private void Update()
        {
            if (FlyingBuilding != null)
            {
                var groundPlane = new Plane(Vector3.right, Vector3.zero);
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (groundPlane.Raycast(ray, out float position))
                {
                    Vector3 worldPosition = ray.GetPoint(position);

                    int x = Mathf.RoundToInt(worldPosition.x);
                    int y = Mathf.RoundToInt(worldPosition.z);

                    bool available = true;

                    if (x < _playerTransform.position.x - Radius || x > Radius - FlyingBuilding.Size.x) available = false;
                    if (y < _playerTransform.position.y - Radius || y > Radius - FlyingBuilding.Size.y) available = false;

                    if (available && IsPlaceTaken(x, y)) available = false;

                    FlyingBuilding.transform.position = new Vector3(x, 0, y);
                    FlyingBuilding.SetTransparent(available);

                    if (available && Input.GetMouseButtonDown(0))
                    {
                        PlaceFlyingBuilding(x, y);
                    }
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

        public void ApplyChanges()
        {
            
        }
    }
}