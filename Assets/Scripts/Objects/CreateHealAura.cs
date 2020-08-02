using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class CreateHealAura : CreateBuilding
    {
        private BuildingsGrid _buildingsGrid;
        public override void Initialize(GameObject obj)
        {
            _buildingsGrid = obj.GetComponent<BuildingsGrid>();
            _buildingsGrid.Initialize();

            _buildingsGrid.Radius = Radius;
        }
        
        public override void TriggerAbility()
        {
            _buildingsGrid.ApplyAbility();
        }
    }
}

