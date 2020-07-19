using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects;
using Controllers;
using Core;

namespace Views
{
    public class WeaponPanelView : BaseView<IWeaponPanelView>, IWeaponPanelView
    {
        protected override IWeaponPanelView View => this;

        public VerticalLayoutGroup VerticalLayoutGroup;
        public WeaponPanel WeaponPanel;

        private Dictionary<WeaponInfo, WeaponPanel> _weaponPanels;

        private void OnEnable()
        {
            _weaponPanels = new Dictionary<WeaponInfo, WeaponPanel>();
        }

        public void InitPanel(WeaponInfo weapon)
        {
            if (weapon == null)
                return;

            _weaponPanels.Add(weapon, Instantiate(WeaponPanel, VerticalLayoutGroup.transform));

            _weaponPanels[weapon].WeaponIco.sprite = weapon.Data.Sprite;

            if (weapon is RangeWeaponInfo)
            {
                RangeWeaponInfo wd = (RangeWeaponInfo)weapon;

                _weaponPanels[weapon].AmmoLeft.text = wd.AmmoLeft.ToString();
                _weaponPanels[weapon].AllAmmo.text = wd.AllAmmo.ToString();
            }
        }

        public void UpdateWeapon(WeaponInfo weapon)
        {
            if (weapon is RangeWeaponInfo)
            {
                RangeWeaponInfo wd = (RangeWeaponInfo)weapon;

                _weaponPanels[weapon].AmmoLeft.text = wd.AmmoLeft.ToString();
                _weaponPanels[weapon].AllAmmo.text = wd.AllAmmo.ToString();

                if (!_weaponPanels[weapon].gameObject.activeSelf && weapon.IsActive)
                {
                    KeyValuePair<WeaponInfo, WeaponPanel> temp = new KeyValuePair<WeaponInfo, WeaponPanel>(weapon, _weaponPanels[weapon]);
                    _weaponPanels.Remove(weapon);
                    _weaponPanels.Add(temp.Key, temp.Value);
                    _weaponPanels[weapon].gameObject.SetActive(true);
                    _weaponPanels[weapon].gameObject.transform.SetParent(VerticalLayoutGroup.transform);
                }
            }
        }

        public void OnWeaponChange(WeaponInfo previous, WeaponInfo current)
        {
            if(previous != null && previous != current)
                if(!previous.IsActive)
                {
                    _weaponPanels[previous].gameObject.SetActive(false);
                    _weaponPanels[previous].gameObject.transform.SetParent(null);
                }
        }
    }
}