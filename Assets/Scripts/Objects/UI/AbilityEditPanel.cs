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

        public void InitPanel(AbilityInfo abilityInfo)
        {
            _panels = new List<LevelChangePanel>();

            _abilityInfo = abilityInfo;
            AbilityIco.sprite = abilityInfo.Ability.aSprite;
            Description.text = abilityInfo.Ability.Name;

            AbilityParametersLayoutGroup.gameObject.SetActive(_abilityInfo.Checked);

            foreach (AbilityPrameter AbilityPrameter in abilityInfo.AbilityPrametersList)
            {
                LevelChangePanel panel = Instantiate(ParameterPanel, AbilityParametersLayoutGroup.transform);
                panel.InitPanel(AbilityPrameter);
                _panels.Add(panel);
            }
        }

        public void OnCheck() //мутатор AbilityInfo
        {
            _abilityInfo.Checked = !_abilityInfo.Checked;
            AbilityParametersLayoutGroup.gameObject.SetActive(_abilityInfo.Checked);
        }
    }
}