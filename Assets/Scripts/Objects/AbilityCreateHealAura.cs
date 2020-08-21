using System.Collections.Generic;
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

            int buildingRadiusLvl = 0;

            if(playerAbilityStats != null)
            {
                buildingRadiusLvl = playerAbilityStats.FindParameterLevel(Parameter.BuildingRadius, Name);
            }

            _buildingsGrid.Radius = Radius + buildingRadiusLvl;
        }
        
        public override void TriggerAbility()
        {
            _buildingsGrid.StartPlacingBuilding(BuildingPrefab);
        }
    }
}

