using System;
using Objects;
using Controllers;
using UnityEngine.UI;

namespace Views
{
    public class AbilityMenuView : BaseView<IAbilityMenuView>, IMenuView
    {
        protected override IAbilityMenuView View => this;

        public List<AbilityStats> AbilityStats { get => _abilityStats; set => _abilityStats = value; }

        [SerializeField] private List<AbilityStats> _abilityStats;

        public VerticalLayoutGroup VerticalLayoutGroup;

        public AbilityPanel AbilityPanel;

        private List<AbilityPanel> _panelsList;

        private void InitPanel()
        {
            foreach(AbilityStats abilityStats in _abilityStats)
            {
                //abilityStats.Ability.
            }
        }
    }
}