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

	public class Weapon : MonoBehaviour
	{
		private WeaponInfo _currentWeapon;
		public WeaponHolder WeaponHolder;

		public bool CanFire => gameObject.activeSelf && _state == WeaponFireState.None;
		public WeaponFireState _state;
		private float _timer;

        public void WeaponChangeEvent(WeaponInfo currentWeapon)
        {
			_currentWeapon = currentWeapon;
			SetWeapon();
		}

		private void SetWeapon()
		{
			if (_currentWeapon.AmmoLeft == 0 && _currentWeapon.AllAmmo > 0 && !_currentWeapon.Data.isMeleWeapon)
			{
				Reloading(_currentWeapon);
			}
            else
            {
				WeaponSetState(WeaponFireState.StartDelay, _currentWeapon.Data.StartDelay);
			}
			WeaponHolder.AnimatorOverrider.Animator.SetFloat("FireTime", _currentWeapon.Data.FireTime*10);
		}

		public void Fire()
		{
			if(_currentWeapon.AmmoLeft > 0)
            {
				WeaponHolder.AnimatorOverrider.Animator.SetTrigger("Attack");
				CreateBullet();
				WeaponSetState(WeaponFireState.DelayBetwenBullets, _currentWeapon.Data.FireTime);
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
				_timer -= Time.deltaTime;
				if(_timer <= 0f)
				{
					if(_state == WeaponFireState.DelayBetwenBullets || _state == WeaponFireState.Reloading || _state == WeaponFireState.StartDelay)
					{
						if (_state == WeaponFireState.Reloading)
							OnReloaded();
						WeaponSetState(WeaponFireState.None, 0f);
					}
				}
			}
			Debug.Log(_state);
		}

		public void Reloading(WeaponInfo weapon)
        {
			if((_currentWeapon == weapon && _currentWeapon.AmmoLeft == 0 || _currentWeapon.AmmoLeft < _currentWeapon.Data.MagazineSize || _currentWeapon.AmmoLeft == 0 && _currentWeapon.AllAmmo > 0) && _state != WeaponFireState.Reloading)
				WeaponSetState(WeaponFireState.Reloading, _currentWeapon.Data.ReloadTime);
		}

		private void OnReloaded()
		{
			_currentWeapon.AddAmmo();
		}

		private void CreateBullet()
		{
			if(_currentWeapon.Data.isMeleWeapon)
            {

            }
            else
            {
				_currentWeapon.CreateBullet(transform);
			}
		}

		private void WeaponSetState(WeaponFireState state, float timer)
        {
			_state = state;
			_timer = timer;
		}
	}
}
