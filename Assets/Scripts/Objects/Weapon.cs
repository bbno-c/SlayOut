using UnityEngine;

namespace Objects
{
	public enum WeaponFireState
	{
		None,
		Firing,
		Reloading
	}

	public class Weapon : MonoBehaviour
	{
		public bool CanFire => gameObject.activeSelf && _state == WeaponFireState.None;
		private WeaponFireState _state;
		private float _timer;
		private int _ammoLeft;

		private void SetWeapon()
		{
			_state = WeaponFireState.Reloading;
			_timer = 1f + WeaponData.StartDelay;
			_ammoLeft = WeaponData.MaxAmmo;
		}

		public void Fire()
		{
			if (!CanFire)
				return;

			_state = WeaponFireState.Firing;
			_timer = WeaponData.FireTime;

			CreateBullet();
		}

		private void Update()
		{
			if(_timer > 0f)
			{
				_timer -= Time.deltaTime;
				if(_timer <= 0f)
				{
					if(_state == WeaponFireState.Firing && (_ammoLeft > 1 || WeaponData.isMeleWeapon))
					{
						_state = WeaponFireState.None;
						_timer = 0f;
						if(!WeaponData.isMeleWeapon)
							_ammoLeft--;
					}
					else if(_state == WeaponFireState.Firing && _ammoLeft > 0)
					{
						_state = WeaponFireState.Reloading;
						_timer = WeaponData.ReloadTime;
					}
					else if(_state == WeaponFireState.Reloading)
					{
						_state = WeaponFireState.None;
						_timer = 0f;
						_ammoLeft = WeaponData.MaxAmmo;
					}
				}
			}
		}

		private void CreateBullet()
		{
			if(WeaponData.isMeleWeapon)
            {

            }
            else
            {
				Instantiate(WeaponData.BulletPrefab, transform.position + new Vector3(0.25f, 0, 0), transform.rotation);
			}
		}
	}
}