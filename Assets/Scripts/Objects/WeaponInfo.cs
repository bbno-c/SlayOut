using System;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public interface WeaponInfo
    {
        WeaponData Data {get;}
        bool IsActive {get;}
    }

    public interface IRangeWeaponInfo
    {
        event Action<WeaponInfo> AmmoPickupEvent;
        event Action<WeaponInfo> AddAmmoEvent;
        event Action<WeaponInfo> CreateBulletEvent;
        bool IsActive { get; }
        int AmmoLeft { get; }
        int AllAmmo { get; }
    }

    public interface IMeleeWeaponInfo
    {
        bool IsActive { get; }
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
        public event Action<WeaponInfo> AmmoPickupEventNotReload;
        public bool IsActive => _ammoLeft + _allAmmo > 0;
        public int AmmoLeft => _ammoLeft;
        public int AllAmmo => _allAmmo;

        public RangeWeaponInfo(RangeWeaponData data, WeaponHolder weaponHolder, List<GameObject> bulletPool)
        {
            _data = data;
            _bulletPool = bulletPool;
            _ammoLeft = data.MagazineSize;
            _allAmmo = data.StartAmmo;

            AmmoPickupEvent += weaponHolder.Weapon.Reloading;
            weaponHolder.ElementExist += PickupAmmo;
        }

        public void PickupAmmo(WeaponInfo currentWeapon)
        {
            _allAmmo += _data.MagazineSize;
            if (_ammoLeft > 0)
                AmmoPickupEventNotReload?.Invoke(this);
            else
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

        public void CreateBullet(Transform weapon, int i)
        {
            if(_ammoLeft > 0)
                foreach(GameObject bullet in _bulletPool)
                    if(!bullet.activeSelf)
                    {
                        bullet.transform.position = weapon.position;
                        if (!_data.IsShotgun)
                        {
                            bullet.transform.rotation = weapon.rotation * Quaternion.Euler(0, 0, UnityEngine.Random.Range(-_data.Spread, _data.Spread));//Quaternion.AngleAxis(UnityEngine.Random.Range(-_data.Spread, _data.Spread), bullet.transform.right);
                        }
                        else
                        {
                            //bullet.transform.Rotate(new Vector3(weapon.rotation.eulerAngles.x, weapon.rotation.eulerAngles.y, weapon.rotation.eulerAngles.z * (-_data.Spread + ((_data.Spread / _data.BulletsPerShot) * i))) );
                            //bullet.transform.rotation = weapon.rotation * Quaternion.Euler(0,0,(-_data.Spread + ((_data.Spread / _data.BulletsPerShot) * i)));//weapon.rotation * Quaternion.AngleAxis((-_data.Spread + (_data.Spread* i) ), bullet.transform.right);
                        }
                        bullet.SetActive(true);
                        _ammoLeft--;
                        CreateBulletEvent?.Invoke(this);
                        return;
                    }
        }
    }

    public class MeleeWeaponInfo: WeaponInfo, IMeleeWeaponInfo
    {
        private MeleeWeaponData _data;
        public WeaponData Data => (WeaponData)_data;

        public bool IsActive => true;

        public MeleeWeaponInfo(MeleeWeaponData data, WeaponHolder weaponHolder)
        {
            _data = data;
        }
    }
}