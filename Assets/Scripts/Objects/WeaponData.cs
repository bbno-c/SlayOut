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
	[System.Serializable]
	[CreateAssetMenu(fileName = "NewRangeWeapon", menuName = "Weapon/RangeWeapon")]
	public class RangeWeaponData : WeaponData
	{
		public GameObject BulletPrefab;
		public float ReloadTime;
		public int MagazineSize;
		public int StartAmmo;
		public int BulletsPerShot;
		public bool IsShotgun;
		public int Spread;
	}
	[System.Serializable]
	[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Weapon/MeleeWeapon")]
	public class MeleeWeaponData : WeaponData
	{
		public float Range;
	}
}
