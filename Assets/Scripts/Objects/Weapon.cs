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
		private WeaponInfo _currentWeapon;

		public bool CanFire => gameObject.activeSelf && _state == WeaponFireState.None;
		private WeaponFireState _state;
		private float _timer;

		public void WeaponChangedEvent(WeaponInfo currentWeapon)
        {
			_currentWeapon = currentWeapon;
			SetWeapon();
		}

		private void SetWeapon()
		{
			if (_currentWeapon.AmmoLeft == 1)
			{
				Reload();
			}
            else
            {
				_state = WeaponFireState.None;
				_timer = 1f + _currentWeapon.Data.StartDelay;
			}
		}

		public void Fire()
		{
			if (!CanFire)
				return;

			_state = WeaponFireState.Firing;
			_timer = _currentWeapon.Data.FireTime;

			CreateBullet();
		}

		private void Update()
		{
			if(_timer > 0f)
			{
				_timer -= Time.deltaTime;
				if(_timer <= 0f)
				{
					if(_state == WeaponFireState.Firing && (_currentWeapon.AmmoLeft > 0 || _currentWeapon.Data.isMeleWeapon))
					{
						_state = WeaponFireState.None;
						_timer = 0f;
						if(!_currentWeapon.Data.isMeleWeapon)
							_currentWeapon.AmmoLeft--;
					}
					else if(_state == WeaponFireState.Firing && _currentWeapon.AmmoLeft == 0)
					{
						_state = WeaponFireState.Reloading;
						_timer = _currentWeapon.Data.ReloadTime;
					}
					else if(_state == WeaponFireState.Reloading)
					{
						_state = WeaponFireState.None;
						_timer = 0f;
						_currentWeapon.AmmoLeft = _currentWeapon.Data.MagazineSize;
					}
				}
			}
		}

		private void Reload()
        {
			_state = WeaponFireState.Reloading;
			_timer = _currentWeapon.Data.ReloadTime;
		}

		private void CreateBullet()
		{
			if(_currentWeapon.Data.isMeleWeapon)
            {

            }
            else
            {
				Instantiate(_currentWeapon.Data.BulletPrefab, transform.position + new Vector3(0.25f, 0, 0), transform.rotation);
			}
		}
	}
}