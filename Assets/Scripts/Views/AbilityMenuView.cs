using System.Collections.Generic;
using Objects;
using Controllers;
using UnityEngine.UI;
using UnityEngine;

namespace Views
{
    public class AbilityMenuView : BaseView<IAbilityMenuView>, IAbilityMenuView
    {
        protected override IAbilityMenuView View => this;

        public List<AbilityInfo> AbilityStats { get => _abilityStatsList; set => _abilityStatsList = value; }
        [SerializeField] private List<AbilityInfo> _abilityStatsList;
        private AbilityStats _abilityStats;

        public VerticalLayoutGroup VerticalLayoutGroup;
        public AbilityEditPanel AbilityEditPanel;
        private Dictionary<AbilityInfo, AbilityEditPanel> _panels;

        private void OnEnable()
        {
            _abilityStats.AbilityStatsList = _abilityStatsList;
        }

        public void InitPanel()
        {
            if (_abilityStats == null)
                return;
            
            foreach(AbilityInfo AbilityInfo in _abilityStatsList)
            {
                _panels.Add(AbilityInfo, Instantiate(AbilityEditPanel, VerticalLayoutGroup.transform));
                _panels[AbilityInfo].InitPanel(AbilityInfo, _abilityStats);
            }
        }
    }
}