using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Character : MonoBehaviour
    {
		public AnimatorOverrider AnimatorOverrider;
		public AbilityHolder AbilityHolder;
		public WeaponHolder WeaponHolder;
        public Movement Movement;
		public Camera Camera;
		public Health Health;
		public Fire Fire;

		private void OnEnable()
		{
			if (Health != null)
			{
				Health.DieEvent += OnDead;
			}
		}

		private void OnDisable()
		{
			if (Health != null)
			{
				Health.DieEvent -= OnDead;
			}
		}

		private void OnDead()
		{
			Destroy(gameObject);
		}
	}
}