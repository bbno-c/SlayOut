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

        public Image AbilityIco;
        public TextMeshProUGUI Description;
        public LevelChangePanel ParameterPanel;
        public VerticalLayoutGroup AbilityParametersLayoutGroup;

        private List<LevelChangePanel> _panels;
        private AbilityInfo _abilityInfo;
        private bool _active;

        public void InitPanel(AbilityInfo abilityInfo, AbilityStats abilityStats)
        {
            _panels = new List<LevelChangePanel>();

            _abilityInfo = abilityInfo;
            _active = abilityInfo.Checked;
            AbilityIco.sprite = abilityInfo.Ability.aSprite;
            AbilityCheckEvent += abilityStats.AbilityChecked;

            foreach (AbilityPrameter AbilityPrameter in abilityInfo.AbilityPrametersList)
            {
                _panels.Add(Instantiate(ParameterPanel, AbilityParametersLayoutGroup.transform));

                LevelChangePanel panel = _panels.Find(x => ParameterPanel);

                panel.InitPanel(AbilityPrameter, abilityInfo);
                panel.LevelUp += abilityStats.AddLevel;
                panel.LevelDown += abilityStats.RemoveLevel;
            }
        }

        public void OnCheck()
        {
            _active = !_active;
            AbilityParametersLayoutGroup.gameObject.SetActive(_active);

            AbilityCheckEvent?.Invoke(_abilityInfo);
        }
    }
}