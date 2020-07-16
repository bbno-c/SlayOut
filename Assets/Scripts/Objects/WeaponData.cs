using UnityEngine;

namespace Objects
{
	public abstract class WeaponData: ScriptableObject
	{
		public AnimationName WeaponAnimationName;
		public Sprite Sprite;
		public float FireTime;
	}

	[CreateAssetMenu(fileName = "NewRangeWeapon", menuName = "Weapon\RangeWeapon")]
	public class RangeWeaponData : WeaponData
	{
		public GameObject BulletPrefab;
		public float StartDelay;
		public float ReloadTime;
		public int MagazineSize;
		public int StartAmmo;
	}

	[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Weapon\MeleeWeapon")]
	public class MeleeWeaponData : WeaponData
	{
		public float StartDelay;
		public float ReloadTime;
	}
}
