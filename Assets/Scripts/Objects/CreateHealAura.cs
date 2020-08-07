using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    [CreateAssetMenu(fileName = "NewCreateHealAura",menuName ="Abilities/CreateHealAura")]
    public class CreateHealAura : CreateBuilding
    {
        private BuildingsGrid _buildingsGrid;
        public override void Initialize(GameObject obj, List<AbilityInfo> abilityStatsList)
        {
            int buildingRadiusLvl = 0;

            if(abilityStatsList != null)
            {
                buildingRadiusLvl = FindParameterLevel(Parameter.BuildingRadius, abilityStatsList);
            }

            _buildingsGrid = obj.GetComponent<BuildingsGrid>();
            _buildingsGrid.Initialize();

            _buildingsGrid.Radius = Radius + buildingRadiusLvl;
        }
        
        public override void TriggerAbility()
        {
            _buildingsGrid.StartPlacingBuilding(BuildingPrefab);
        }
    }
}

