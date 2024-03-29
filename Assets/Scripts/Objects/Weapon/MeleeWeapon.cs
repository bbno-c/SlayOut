using System;
using UnityEngine;
using Unity.Mathematics;

namespace Objects
{
	public class MeleeWeapon : MonoBehaviour
	{
		public Transform player;
		private MeleeWeaponInfo _currentWeapon;
		private WeaponDataMelee _currentWeaponDataMelee;
		public WeaponHolder WeaponHolder;
		public SpriteRenderer SpriteRenderer;
		private bool _reverse;

		public bool CanFire => gameObject.activeSelf && _state == WeaponFireState.None && _isMeleeWeapon && WeaponHolder.AnimatorOverrider.Animator.GetCurrentAnimatorStateInfo(0).IsName("WALK");
		private WeaponFireState _state;
		private float _timer;
		private bool _isMeleeWeapon;

        public void WeaponChangeEvent(WeaponInfo currentWeapon)
        {
			MeleeWeaponInfo weapon = currentWeapon as MeleeWeaponInfo;
			if(weapon == null)
			{
				_isMeleeWeapon = false;
				SpriteRenderer.flipY = false;
				return;
			}
			else
			{
				_isMeleeWeapon = true;
				_currentWeapon = weapon;
				_currentWeaponDataMelee = (WeaponDataMelee)weapon.Data;
				SpriteRenderer.flipY = _reverse;
				SetWeapon();
			}
		}

		private void SetWeapon()
		{
			WeaponSetState(WeaponFireState.StartDelay, _currentWeaponDataMelee.StartDelay);
			WeaponHolder.AnimatorOverrider.Animator.SetFloat("FireTime", _currentWeaponDataMelee.FireTime *_currentWeaponDataMelee.AnimationMultiplier);
		}

		public void Fire()
		{
			WeaponHolder.AnimatorOverrider.Animator.SetTrigger("Attack");
			CreateBullet();
			WeaponSetState(WeaponFireState.DelayBetwenBullets, _currentWeaponDataMelee.FireTime);
			_reverse = !_reverse;
			SpriteRenderer.flipY = _reverse;
		}

		private void Update()
		{
			if (_timer > 0f)
			{
				_timer -= Time.deltaTime;
				if(_timer <= 0f)
				{
					//if(_state == WeaponFireState.DelayBetwenBullets || _state == WeaponFireState.StartDelay)
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

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;

			var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
			var angle = Mathf.Atan2(dir.y, dir.x);

			Gizmos.DrawLine(
				new Vector2(transform.position.x, transform.position.y),
				new Vector2(transform.position.x + math.cos(angle) * _currentWeaponDataMelee.Range,
							transform.position.y + math.sin(angle) * _currentWeaponDataMelee.Range));
		}
	}
}
