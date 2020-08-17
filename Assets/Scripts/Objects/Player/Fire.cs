using System;
using UnityEngine;

namespace Objects
{
	public enum FireType
	{
		Shoot
	}

	[Serializable]
	public class FireInfo
	{
		public FireType FireType;
		public RangeWeapon[] RangeWeapons;
		public MeleeWeapon[] MeleeWeapons;
	}

	public class Fire : MonoBehaviour
	{
		public FireInfo[] FireInfos;

		public void Apply(FireType fireType)
		{
			foreach(var info in FireInfos)
				if (info.FireType == fireType)
                {
					foreach (var weapon in info.RangeWeapons)
						if (weapon.CanFire)
							weapon.Fire();

					foreach (var weapon in info.MeleeWeapons)
						if (weapon.CanFire)
							weapon.Fire();
				}
		}
	}
}