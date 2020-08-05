using System;
using UnityEngine;

namespace Objects
{
    public class ParameterPanel : MonoBehaviour
    {
        private AbilityStats _abilityStats;

        public void InitPanel(AbilityPrameter abilityPrameter, AbilityStats abilityStats)
        {
            _abilityStats = abilityStats;

            
        }
    }
}