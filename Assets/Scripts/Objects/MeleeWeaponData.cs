using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
	[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Weapon/MeleeWeapon")]
	public class MeleeWeaponData : WeaponData
	{
		public float Range;
	}
}