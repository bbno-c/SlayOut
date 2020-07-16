using System;
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
        public WeaponPanel WeaponPanel;

        private Dictionary<WeaponInfo, WeaponPanel> _weaponPanels;

        public void InitPanel(WeaponInfo weapon)
        {
            _weaponPanels.Add(weapon, Instantiate(WeaponPanel));
            VerticalLayoutGroup.Add(_weaponPanels[weapon]);
        }

        public void UpdateWeapon(WeaponInfo weapon)
        {
            _weaponPanels[weapon].AmmoLeft.Text = weapon.AmmoLeft.ToString();
            _weaponPanels[weapon].AllAmmo.Text = weapon.AllAmmo.ToString();

            if(!_weaponPanels[weapon].ActiveSelf && weapon.IsActive)
            {
                KeyValuePair<WeaponInfo, WeaponPanel> temp = new KeyValuePair(weapon, _weaponPanels[weapon]);
                _weaponPanels.Remove(weapon);
                _weaponPanels.Add(temp.Key, temp.Value);
                _weaponPanels[weapon].SetActive(true);
                VerticalLayoutGroup.Add(_weaponPanels[weapon]);
            }
        }

        public void OnWeaponChange(WeaponInfo previous, WeaponInfo current)
        {
            if(!previous.IsActive)
            {
                _weaponPanels[previous].SetActive(false);
                VerticalLayoutGroup.Remove(_weaponPanels[previous]);
            }
        }
    }
}