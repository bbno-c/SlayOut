using System;
using UnityEngine;

namespace Objects
{
	public class MeleeWeapon : MonoBehaviour
	{
		private MeleeWeaponInfo _currentWeapon;
		private MeleeWeaponData _currentMeleeWeaponData;
		public WeaponHolder WeaponHolder;
		public event Action<WeaponInfo> ReloadingEvent;

		public bool CanFire => gameObject.activeSelf && _state == WeaponFireState.None && _isMeleeWeapon;
		public WeaponFireState _state;
		private float _timer;
		private bool _isMeleeWeapon = true;

        public void WeaponChangeEvent(WeaponInfo currentWeapon)
        {
			MeleeWeaponInfo weapon = currentWeapon as MeleeWeaponInfo;
			if(weapon == null)
			{
				_isMeleeWeapon = false;
				return;
			} else
			{
				_currentWeapon = weapon;
				_currentMeleeWeaponData = (MeleeWeaponData)weapon.Data;
			}

			SetWeapon();
		}

		private void SetWeapon()
		{
			WeaponSetState(WeaponFireState.StartDelay, _currentMeleeWeaponData.StartDelay);
			WeaponHolder.AnimatorOverrider.Animator.SetFloat("FireTime", _currentMeleeWeaponData.FireTime*10);
		}

		public void Fire()
		{
			WeaponHolder.AnimatorOverrider.Animator.SetTrigger("Attack");
			CreateBullet();
			WeaponSetState(WeaponFireState.DelayBetwenBullets, _currentMeleeWeaponData.FireTime);
		}

		private void Update()
		{
			if (_timer > 0f)
			{
				_timer -= Time.deltaTime;
				if(_timer <= 0f)
				{
					if(_state == WeaponFireState.DelayBetwenBullets || _state == WeaponFireState.StartDelay)
					{
						WeaponSetState(WeaponFireState.None, 0f);
					}
				}
			}
			//Debug.Log(_state);
		}

		private void CreateBullet()
		{
			_currentWeapon.CreateBullet(transform);
		}

		private void WeaponSetState(WeaponFireState state, float timer)
        {
			_state = state;
			_timer = timer;
		}
	}
}
