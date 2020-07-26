using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Controllers
{
    public interface IWeaponStateBarView : IView
    {
        void SetTimer(float time);
    }

    public class WeaponStateBarController : IController<IWeaponStateBarView>
    {
        private readonly IGame _game;
        private IWeaponStateBarView _view;

        public WeaponStateBarController(IGame game)
        {
            _game = game;
        }

        public void OnOpen(IWeaponStateBarView view)
        {
            _game.Player.WeaponHolder.WeaponChange += OnSwitching;
            _game.Player.WeaponHolder.RangeWeapon.ReloadingEvent += OnReload;
            _view = view;
        }

        public void OnClose(IWeaponStateBarView view)
        {
            _game.Player.WeaponHolder.WeaponChange -= OnSwitching;
            _game.Player.WeaponHolder.WeaponChange -= OnReload;
            _view = null;
        }

        private void OnReload(WeaponInfo weapon)
        {
            float time = 0;
            if (weapon is RangeWeaponInfo)
            {
                RangeWeaponInfo wd = (RangeWeaponInfo)weapon;
                RangeWeaponData wp = (RangeWeaponData)wd.Data;
                time = wp.ReloadTime;
            }
            _view.SetTimer(time);
        }

        private void OnSwitching(WeaponInfo weapon)
        {
            float time = weapon.Data.StartDelay;
            _view.SetTimer(time);
        }
    }
}