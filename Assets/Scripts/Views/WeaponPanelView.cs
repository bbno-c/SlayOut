using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects;
using Controllers;

namespace Views
{
    public class WeaponPanelView : BaseView<IWeaponPanelView>, IWeaponPanelView
    {
        protected override IWeaponPanelView View => this;

        public VerticalLayoutGroup VerticalLayoutGroup;
        public GameObject WeaponPanel;

        private Dictionary<WeaponInfo, GameObject> _weaponPanels;

        public void InitPanel(WeaponInfo weapon)
        {
            
        }
    }
}