using UnityEngine;

namespace Objects
{
    [CreateAssetMenu(fileName = "NewCreateHealAura",menuName ="Abilities/CreateHealAura")]
    public class AbilityCreateHealAura : AbilityCreateBuilding
    {
        private BuildingsGrid _buildingsGrid;

        public override void Initialize(GameObject obj, AbilityStats playerAbilityStats)
        {
            _buildingsGrid = obj.GetComponent<BuildingsGrid>();

            if (!_buildingsGrid)
                return;

            _buildingsGrid?.Initialize();

            _buildingsGrid.Radius = Radius + playerAbilityStats.FindParameterLevel(Parameter.BuildingRadius, Name);
        }
        
        public override void TriggerAbility()
        {
            _buildingsGrid.StartPlacingBuilding(BuildingPrefab);
        }
    }
}

