using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Objects
{
    public class AbilityEditPanel:MonoBehaviour
    {
        public event Action<AbilityInfo> AbilityCheckEvent;

        public VerticalLayoutGroup AbilityParametersLayoutGroup;
        public Image AbilityIco;
        public TextMeshProUGUI Description;
        public LevelChangePanel ParameterPanel;

        private List<LevelChangePanel> _panels;
        private AbilityInfo _abilityInfo;
        private bool _active;

        public void InitPanel(AbilityInfo abilityInfo, AbilityStats abilityStats)
        {
            _active = true;
            _abilityInfo = abilityInfo;
            AbilityIco.sprite = abilityInfo.Ability.aSprite;
            AbilityCheckEvent += abilityStats.AbilityChecked;

            foreach (AbilityPrameter AbilityPrameter in abilityInfo.AbilityPrametersList)
            {
                _panels.Add(Instantiate(ParameterPanel, AbilityParametersLayoutGroup.transform));

                _panels[_panels.LastIndexOf(ParameterPanel)].InitPanel(AbilityPrameter, abilityInfo);
                _panels[_panels.LastIndexOf(ParameterPanel)].LevelUp += abilityStats.AddLevel;
                _panels[_panels.LastIndexOf(ParameterPanel)].LevelDown += abilityStats.RemoveLevel;
            }
        }

        public void OnCheck()
        {
            _active = !_active;
            AbilityCheckEvent?.Invoke(_abilityInfo);
            AbilityParametersLayoutGroup.gameObject.SetActive(_active);
        }
    }
}