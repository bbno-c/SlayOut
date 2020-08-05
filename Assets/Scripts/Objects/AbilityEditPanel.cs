using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Objects
{
    public class AbilityEditPanel:MonoBehaviour
    {
        public VerticalLayoutGroup VerticalLayoutGroup;

        public Image AbilityIco;
        public TextMeshProUGUI Description;
        public ParameterPanel ParameterPanel;

        private AbilityStats _abilityStats;
        private List<ParameterPanel> _panels;

        public void InitPanel(AbilityInfo AbilityInfo, AbilityStats abilityStats)
        {
            _abilityStats = abilityStats;

            foreach (AbilityPrameter AbilityPrameter in AbilityInfo.AbilityPrametersList)
            {
                _panels.Add(Instantiate(ParameterPanel, VerticalLayoutGroup.transform));
            }
        }
    }
}
