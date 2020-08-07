using System;
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

        private AbilityStats _abilityStats;

        [SerializeField]
        private MenuView _menuView;
        public IMenuView MenuView => _menuView;

        [SerializeField]
        private List<AbilityInfo> _abilityStatsList;
        public List<AbilityInfo> AbilityStatsList { get => _abilityStatsList; set => _abilityStatsList = value; }

        public event Action DiscardEvent;
        public event Action SaveEvent;

        public VerticalLayoutGroup AbilityPanelsLayoutGroup;
        public AbilityEditPanel AbilityEditPanel;

        private List<AbilityEditPanel> _panels;

        private void OnEnable()
        {
            _abilityStats = new AbilityStats();
            _abilityStats.AbilityStatsList = _abilityStatsList;
        }

        public void InitPanel()
        {
            if (_abilityStats == null)
                return;
            
            foreach(AbilityInfo AbilityInfo in _abilityStatsList)
            {
                _panels.Add(Instantiate(AbilityEditPanel, AbilityPanelsLayoutGroup.transform));
                _panels[_panels.LastIndexOf(AbilityEditPanel)].InitPanel(AbilityInfo, _abilityStats);
            }
        }

        public void ActionDiscard()
        {
            DiscardEvent?.Invoke();
        }

        public void ActionSave()
        {
            SaveEvent?.Invoke();
        }
    }
}