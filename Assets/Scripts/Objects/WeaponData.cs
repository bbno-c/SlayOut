using UnityEngine;

namespace Objects
{
	[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
	public class WeaponData : ScriptableObject
	{
		public AnimationName WeaponName;
		public bool isMeleWeapon;

		public GameObject BulletPrefab;
		public float StartDelay;
		public float FireTime;
		public float ReloadTime;
		public int MagazineSize;
		public Sprite SpawnerSprite;
	}
}
