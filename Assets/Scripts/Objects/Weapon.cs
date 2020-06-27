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
		public GameObject BulletPrefab;
		public float StartDelay = 0f;
		public float FireTime = 0.3f;
		public float ReloadTime = -1f;
		public int Count;

		private WeaponFireState _state;
		private float _timer;
		private int _count;

		public bool CanFire => gameObject.activeSelf && _state == WeaponFireState.None;

		private void OnEnable()
		{
			_state = WeaponFireState.Reloading;
			_timer = 1f + StartDelay;
			_count = 0;
		}

		public void Fire()
		{
			if (!CanFire)
				return;

			_state = WeaponFireState.Firing;
			_timer = FireTime;
			_count = Count;

			CreateBullet();
		}

		private void Update()
		{
			if(_timer > 0f)
			{
				_timer -= Time.deltaTime;
				if(_timer <= 0f)
				{
					if(_state == WeaponFireState.Firing && _count > 1)
					{
						_timer = FireTime;
						_count--;

						CreateBullet();
					}
					else if(_state == WeaponFireState.Firing && Count > 0)
					{
						_state = WeaponFireState.Reloading;
						_timer = ReloadTime;
						_count = 0;
					}
					else if(_state == WeaponFireState.Reloading || Count <= 0)
					{
						_state = WeaponFireState.None;
						_timer = 0f;
						_count = 0;
					}
				}
			}
		}

		private void CreateBullet()
		{
			Instantiate(BulletPrefab, transform.position + new Vector3(0.25f,0,0), transform.rotation);
		}
	}
}