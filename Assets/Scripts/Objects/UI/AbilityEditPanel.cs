using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Objects
{
    public class AbilityEditPanel:MonoBehaviour
    {
        public event Action<AbilityInfo> AbilityCheked;

        public VerticalLayoutGroup VerticalLayoutGroup;
        public Image AbilityIco;
        public TextMeshProUGUI Description;
        public LevelChangePanel ParameterPanel;

        private List<LevelChangePanel> _panels;
        private AbilityInfo _abilityInfo;
        private bool _active;

        public void InitPanel(AbilityInfo abilityInfo, AbilityStats abilityStats)
        {
            _abilityInfo = abilityInfo;

            foreach (AbilityPrameter AbilityPrameter in abilityInfo.AbilityPrametersList)
            {
                _panels.Add(Instantiate(ParameterPanel, VerticalLayoutGroup.transform));
                _panels[_panels.LastIndexOf(ParameterPanel)].InitPanel(AbilityPrameter, abilityInfo);

                _panels[_panels.LastIndexOf(ParameterPanel)].LevelUp += abilityStats.AddLevel;
                _panels[_panels.LastIndexOf(ParameterPanel)].LevelDown += abilityStats.RemoveLevel;
            }

            AbilityCheked += abilityStats.AbilityChecked;

            _active = true;

            AbilityIco.sprite = abilityInfo.Ability.aSprite;
        }

        public void OnCheck()
        {
            _active = !_active;
            AbilityCheked?.Invoke(_abilityInfo);
            VerticalLayoutGroup.gameObject.SetActive(_active);
        }
    }
}