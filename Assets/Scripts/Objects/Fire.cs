using System;
using UnityEngine;

namespace Objects
{
	public enum FireType
	{
		Shoot,
		Throw,
		Punch
	}

	[Serializable]
	public class FireInfo
	{
		public FireType FireType;
		public Weapon[] Weapons;
	}

	public class Fire : MonoBehaviour
	{
		public FireInfo[] FireInfos;

		public void Apply(FireType fireType)
		{
			foreach(var info in FireInfos)
				if (info.FireType == fireType)
					foreach (var weapon in info.Weapons)
						if (weapon.CanFire)
							weapon.Fire();
		}
	}
}