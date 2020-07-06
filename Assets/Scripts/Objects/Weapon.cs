using System;
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

		public bool CanFire => gameObject.activeSelf && _state == WeaponFireState.None && _currentWeapon.AmmoLeft > 0;
		private WeaponFireState _state;
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
				Reloading();
			}
            else
            {
				_state = WeaponFireState.StartDelay;
				_timer = _currentWeapon.Data.StartDelay;
			}
			WeaponHolder.AnimatorOverrider.Animator.SetFloat("FireTime", _currentWeapon.Data.FireTime*10);
		}

		public void Fire()
		{
			if (!CanFire)
            {
				if (_currentWeapon.AmmoLeft == 0 && _state != WeaponFireState.Reloading)
					Reloading();
				return;
			}

			WeaponHolder.AnimatorOverrider.Animator.SetTrigger("Attack");

			_state = WeaponFireState.DelayBetwenBullets;
			_timer = _currentWeapon.Data.FireTime;

			CreateBullet();
		}

		private void Update()
		{
			if (_timer > 0f)
			{
				_timer -= Time.deltaTime;
				if(_timer <= 0f)
				{
					if(_state == WeaponFireState.DelayBetwenBullets && (_currentWeapon.AmmoLeft > 0 || _currentWeapon.Data.isMeleWeapon))
					{
						_state = WeaponFireState.None;
						_timer = 0f;
						if(!_currentWeapon.Data.isMeleWeapon)
							_currentWeapon.AmmoLeft--;
					}
					else if(_state == WeaponFireState.Reloading || _state == WeaponFireState.StartDelay)
					{
						_state = WeaponFireState.None;
						_timer = 0f;
						if(_state == WeaponFireState.Reloading)
						{
							OnReloaded();
						}
					}
				}
			}
		}

		private void Reloading()
        {
			_state = WeaponFireState.Reloading;
			_timer = _currentWeapon.Data.ReloadTime;
		}

		public void ReloadingOnAmmoPickup(WeaponInfo weapon)
		{
			if(weapon == _currentWeapon)
            {
				_state = WeaponFireState.Reloading;
				_timer = _currentWeapon.Data.ReloadTime;
			}
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
	}
}
