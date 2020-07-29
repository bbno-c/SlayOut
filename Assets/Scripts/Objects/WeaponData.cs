using UnityEngine;

namespace Objects
{
	[System.Serializable]
	public abstract class WeaponData: ScriptableObject
	{
		public AnimationName WeaponAnimationName;
		public Sprite Sprite;
		public float StartDelay;
		public float FireTime;
		public float AnimationMultiplier;
	}
}
