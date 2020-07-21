using UnityEngine;

namespace Objects
{
	[System.Serializable]
	public abstract class WeaponData: ScriptableObject
	{
		public AnimationName WeaponAnimationName;
		public Sprite Sprite;
		public float FireTime;
		public float AnimationMultiplier;
	}

	[CreateAssetMenu(fileName = "NewRangeWeapon", menuName = "Weapon/RangeWeapon")]
	public class RangeWeaponData : WeaponData
	{
		public GameObject BulletPrefab;
		public float StartDelay;
		public float ReloadTime;
		public int MagazineSize;
		public int StartAmmo;
		public int BulletsPerShot;
		public bool IsShotgun;
		public int Spread;
	}

	[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Weapon/MeleeWeapon")]
	public class MeleeWeaponData : WeaponData
	{
		public float StartDelay;
		public float ReloadTime;
	}
}
