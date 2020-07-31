using UnityEngine;

namespace Objects
{
    public class BuildingsGrid : MonoBehaviour, IAbility
    {
        public Vector2Int GridSize = new Vector2Int(10, 10);

        private Building[,] grid;
        private Building flyingBuilding;
        private Camera mainCamera;

        private Transform _playerTransform;
        private float _radius;
        
        private void Awake()
        {
            grid = new Building[GridSize.x, GridSize.y];
            
            mainCamera = Camera.main;
        }

        public void ApplyAbility()
        {
            StartPlacingBuilding(buildingPrefab);
        }

        public void StartPlacingBuilding(Building buildingPrefab)
        {
            if (flyingBuilding != null)
            {
                Destroy(flyingBuilding.gameObject);
            }
            
            flyingBuilding = Instantiate(buildingPrefab);
        }

        private void Update()
        {
            if (flyingBuilding != null)
            {
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (groundPlane.Raycast(ray, out float position))
                {
                    Vector3 worldPosition = ray.GetPoint(position);

                    int x = Mathf.RoundToInt(worldPosition.x);
                    int y = Mathf.RoundToInt(worldPosition.z);

                    bool available = true;

                    if (x < _playerTransform.position.x - _radius || x > _radius - flyingBuilding.Size.x) available = false;
                    if (y < _playerTransform.position.y - _radius || y > _radius - flyingBuilding.Size.y) available = false;

                    if (available && IsPlaceTaken(x, y)) available = false;

                    flyingBuilding.transform.position = new Vector3(x, 0, y);
                    flyingBuilding.SetTransparent(available);

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
            flyingBuilding.SetNormal();
            flyingBuilding = null;
        }
    }
}