using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public interface WeaponInfo
    {
        public WeaponData Data {get;}
        public bool IsActive {get;}
    }

    public interface IRangeWeaponInfo
    {
        public event Action<WeaponInfo> AmmoPickupEvent;
        public event Action<WeaponInfo> AddAmmoEvent;
        public event Action<WeaponInfo> CreateBulletEvent;
        public bool IsActive { get; }
        public int AmmoLeft { get; }
        public int AllAmmo { get; }
    }

    public class RangeWeaponInfo: WeaponInfo, IRangeWeaponInfo
    {
        private RangeWeaponData _data;
        public WeaponData Data => (WeaponData)_data;
        private int _ammoLeft;
        private int _allAmmo;
        private List<GameObject> _bulletPool;
        public event Action<WeaponInfo> AmmoPickupEvent;
        public event Action<WeaponInfo>  AddAmmoEvent;
        public event Action<WeaponInfo>  CreateBulletEvent;
        public bool IsActive => _ammoLeft + _allAmmo > 0;
        public int AmmoLeft => _ammoLeft;
        public int AllAmmo => _ammoLeft;

        public RangeWeaponInfo(RangeWeaponData data, WeaponHolder weaponHolder)
        {
            _data = data;

            var bulletPool = new List<GameObject>();
            for (int i = 0; i < data.MagazineSize; i++)
            {
                bulletPool.Add(Instantiate(data.BulletPrefab));
                bulletPool[i].SetActive(false);
            }
            
            _bulletPool = bulletPool;
            _ammoLeft = data.MagazineSize;
            _allAmmo = data.StartAmmo;

            AmmoPickupEvent += weaponHolder.Weapon.Reloading;
            weaponHolder.ElementExist += PickupAmmo;
        }

        public void PickupAmmo(WeaponInfo currentWeapon)
        {
            _allAmmo += _data.MagazineSize;
            AmmoPickupEvent?.Invoke(this);
        }
        
        public void AddAmmo()
        {
            int diff = _data.MagazineSize - _ammoLeft;
            if(_allAmmo - diff >= 0)
            {
                _allAmmo -= diff;
                _ammoLeft += diff;
            }
            else
            {
                _ammoLeft = _allAmmo;
                _allAmmo = 0;
            }
            AddAmmoEvent?.Invoke(this);
        }

        public void CreateBullet(Transform weapon)
        {
            if(_ammoLeft > 0)
                foreach(GameObject bullet in _bulletPool)
                    if(!bullet.activeSelf)
                    {
                        bullet.transform.position = weapon.position;
                        bullet.transform.rotation = weapon.rotation;
                        bullet.SetActive(true);
                        _ammoLeft--;
                        CreateBulletEvent?.Invoke(this);
                        return;
                    }
        }
    }
}