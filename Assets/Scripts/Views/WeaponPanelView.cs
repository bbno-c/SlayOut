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
            else if(weapon is MeleeWeaponInfo)
            {
                _weaponPanels[weapon].AmmoLeft.gameObject.SetActive(false);
                _weaponPanels[weapon].AllAmmo.gameObject.SetActive(false);
                _weaponPanels[weapon].DivideSymbol.gameObject.SetActive(false);
            }
            _weaponPanels[weapon].gameObject.transform.SetAsFirstSibling();
        }

        public void UpdateWeapon(WeaponInfo weapon)
        {
            RangeWeaponInfo wd = weapon as RangeWeaponInfo;
            if (wd != null)
            {
                _weaponPanels[weapon].AmmoLeft.text = wd.AmmoLeft.ToString();
                _weaponPanels[weapon].AllAmmo.text = wd.AllAmmo.ToString();

                if(!weapon.IsActive)
                    _weaponPanels[wd].Image.color = Color.red;

                if (!_weaponPanels[weapon].gameObject.activeSelf && weapon.IsActive)
                {
                    KeyValuePair<WeaponInfo, WeaponPanel> temp = new KeyValuePair<WeaponInfo, WeaponPanel>(weapon, _weaponPanels[weapon]);
                    _weaponPanels.Remove(weapon);
                    _weaponPanels.Add(temp.Key, temp.Value);
                    _weaponPanels[weapon].gameObject.SetActive(true);
                    _weaponPanels[weapon].gameObject.transform.SetParent(VerticalLayoutGroup.transform);
                    _weaponPanels[weapon].gameObject.transform.SetAsFirstSibling();
                }
            }
        }

        public void OnWeaponChange(WeaponInfo previous, WeaponInfo current)
        {
            if(previous != null)
                _weaponPanels[previous].Image.color = Color.gray;

            _weaponPanels[current].Image.color = Color.green;

            if (previous != null && previous != current)
                if(!previous.IsActive)
                {
                    _weaponPanels[previous].gameObject.SetActive(false);
                    _weaponPanels[previous].gameObject.transform.SetParent(null);
                }
        }
    }
}