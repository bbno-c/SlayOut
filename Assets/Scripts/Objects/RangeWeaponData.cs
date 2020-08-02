using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
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
}