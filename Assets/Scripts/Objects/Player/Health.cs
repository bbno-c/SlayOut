using System;
using UnityEngine;

namespace Objects
{
	public class Health : MonoBehaviour
	{
		public event Action DieEvent;
		public event Action<float> ChangeEvent;
		public float HitpointMax;
		public float Hitpoints { get; private set; }

		private void OnEnable()
		{
			Hitpoints = HitpointMax;
		}

		public void Damage(float value)
		{
			Hitpoints = Mathf.Max(0f, Hitpoints - value);

			ChangeEvent?.Invoke(Hitpoints);

			if (Hitpoints > 0f) 
				return;

			DieEvent?.Invoke();
		}
	}
}
