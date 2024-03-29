﻿using System;
using UnityEngine;

namespace Objects
{
    public enum WeaponFireState
    {
        None,
        DelayBetwenBullets,
        Reloading,
        StartDelay
    }

    public class RangeWeapon : MonoBehaviour
    {
        private RangeWeaponInfo _currentWeapon;
        private WeaponDataRange _currentWeaponDataRange;
        public WeaponHolder WeaponHolder;
        public event Action<WeaponInfo> ReloadingEvent;

        public bool CanFire => gameObject.activeSelf && _state == WeaponFireState.None && _isRangeWeapon && WeaponHolder.AnimatorOverrider.Animator.GetCurrentAnimatorStateInfo(0).IsName("WALK");
        private WeaponFireState _state;
        private float _timer;
        private bool _isRangeWeapon;

        public void WeaponChangeEvent(WeaponInfo currentWeapon)
        {
            RangeWeaponInfo weapon = currentWeapon as RangeWeaponInfo;
            if (weapon == null)
            {
                _isRangeWeapon = false;
                WeaponSetState(WeaponFireState.None, 0f);
                return;
            }
            else
            {
                _isRangeWeapon = true;
                _currentWeapon = weapon;
                _currentWeaponDataRange = (WeaponDataRange)weapon.Data;
                SetWeapon();
            }
        }

        private void SetWeapon()
        {
            if (_currentWeapon.AmmoLeft == 0 && _currentWeapon.AllAmmo > 0)
            {
                Reloading(_currentWeapon);
            }
            else
            {
                WeaponSetState(WeaponFireState.StartDelay, _currentWeaponDataRange.StartDelay);
            }
            WeaponHolder.AnimatorOverrider.Animator.SetFloat("FireTime", _currentWeaponDataRange.FireTime * _currentWeaponDataRange.AnimationMultiplier);
        }

        public void Fire()
        {
            if (_currentWeapon.AmmoLeft > 0)
            {
                WeaponHolder.AnimatorOverrider.Animator.SetTrigger("Attack");
                for (int i = 0; i < _currentWeaponDataRange.BulletsPerShot; i++)
                    CreateBullet(i);
                WeaponSetState(WeaponFireState.DelayBetwenBullets, _currentWeaponDataRange.FireTime);
            }
            else
            {
                Reloading(_currentWeapon);
            }
        }

        private void Update()
        {
            if (_timer > 0f)
            {
                if (_state == WeaponFireState.DelayBetwenBullets && _currentWeapon.AmmoLeft == 0 && _currentWeapon.AllAmmo > 0)
                    Reloading(_currentWeapon);

                _timer -= Time.deltaTime;
                if (_timer <= 0f)
                {
                    if (_state == WeaponFireState.DelayBetwenBullets || _state == WeaponFireState.Reloading || _state == WeaponFireState.StartDelay)
                    {
                        if (_state == WeaponFireState.Reloading)
                            OnReloaded();

                        WeaponSetState(WeaponFireState.None, 0f);
                    }
                }
            }
            //Debug.Log(_state);
        }

        public void Reloading(WeaponInfo weapon)
        {
            if ((_currentWeapon == weapon && _currentWeapon.AmmoLeft < _currentWeaponDataRange.MagazineSize && _currentWeapon.AllAmmo > 0) && _state != WeaponFireState.Reloading)
            {
                WeaponSetState(WeaponFireState.Reloading, _currentWeaponDataRange.ReloadTime);
                ReloadingEvent.Invoke(_currentWeapon);
            }
        }

        private void OnReloaded()
        {
            _currentWeapon.AddAmmo();
        }

        private void CreateBullet(int i)
        {
            _currentWeapon.CreateBullet(transform, i);
        }

        private void WeaponSetState(WeaponFireState state, float timer)
        {
            _state = state;
            _timer = timer;
        }
    }
}
