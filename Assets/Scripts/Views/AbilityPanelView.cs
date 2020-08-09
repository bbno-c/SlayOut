using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects;
using Controllers;

namespace Views
{
    public class AbilityPanelView : BaseView<IAbilityPanelView>, IAbilityPanelView
    {
        protected override IAbilityPanelView View => this;

        public HorizontalLayoutGroup AbilityPanelsLayoutGroup;
        public AbilityPanel AbilityPanel;

        public void InitPanel()
        {

        }
    }
}