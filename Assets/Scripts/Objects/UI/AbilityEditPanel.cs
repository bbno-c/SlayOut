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

        public void InitPanel(AbilityInfo abilityInfo)
        {
            _panels = new List<LevelChangePanel>();

            _abilityInfo = abilityInfo;
            _active = abilityInfo.Checked;
            AbilityIco.sprite = abilityInfo.Ability.aSprite;

            foreach (AbilityPrameter AbilityPrameter in abilityInfo.AbilityPrametersList)
            {
                LevelChangePanel panel = Instantiate(ParameterPanel, AbilityParametersLayoutGroup.transform);
                panel.InitPanel(AbilityPrameter);
                _panels.Add(panel);
            }
        }

        public void OnCheck() //мутатор AbilityInfo
        {
            _active = !_active;
            AbilityParametersLayoutGroup.gameObject.SetActive(_active);
        }
    }
}