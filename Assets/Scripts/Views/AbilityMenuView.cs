using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects;
using Controllers;

namespace Views
{
    public class AbilityMenuView : BaseView<IAbilityMenuView>, IAbilityMenuView
    {
        protected override IAbilityMenuView View => this;

        private AbilityStats _abilityStats;
        public AbilityStats AbilityStats { get => _abilityStats; set => _abilityStats = value; }

        [SerializeField]
        private MenuView _menuView;
        public IMenuView MenuView => _menuView;

        public event Action DiscardEvent;
        public event Action SaveEvent;

        public VerticalLayoutGroup AbilityPanelsLayoutGroup;
        public AbilityEditPanel AbilityEditPanel;
        private List<AbilityEditPanel> _panels;

        private void OnDisable()
        {
            foreach (AbilityEditPanel abilityEditPanel in _panels)
                Destroy(abilityEditPanel.gameObject);
        }

        public void InitPanel()
        {
            if (_abilityStats == null)
                return;

            _panels = new List<AbilityEditPanel>();

            foreach (AbilityInfo AbilityInfo in _abilityStats.AbilityStatsList)
            {
                _panels.Add(Instantiate(AbilityEditPanel, AbilityPanelsLayoutGroup.transform));
                _panels.Find(x => AbilityEditPanel).InitPanel(AbilityInfo, _abilityStats);
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