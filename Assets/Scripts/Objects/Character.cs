using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Character : MonoBehaviour
    {
        public Movement Movement;
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