using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
	[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Weapon/MeleeWeapon")]
	public class WeaponDataMelee : WeaponData
	{
		public float Range;
	}
}