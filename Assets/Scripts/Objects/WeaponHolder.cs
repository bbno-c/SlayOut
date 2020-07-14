using Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    

    private List<WeaponInfo> _playerWeapons;
    private WeaponInfo _currentWeapon;
    private int _currentIndex;

    public Weapon Weapon;
    public AnimatorOverrider AnimatorOverrider;
    public WeaponData StartWeapon;
    public event Action<WeaponInfo> WeaponChange;
    public event Action<WeaponInfo> ElementAdded;

    private void Start()
    {
        _playerWeapons = new List<WeaponInfo>();

        WeaponChange += Weapon.WeaponChangeEvent;
        WeaponChange += AnimatorOverrider.ChangeWeaponAnimation;

        AddElement(StartWeapon);
        _currentIndex = 0;
        _currentWeapon = _playerWeapons[_currentIndex];
        OnWeaponChange();
    }

    private void OnWeaponChange()
    {
        WeaponChange?.Invoke(_currentWeapon);
    }
    
    private void OnAddElement(WeaponData element)
    {
        ElementAdded?.Invoke(element);
    }

    public void AddElement(WeaponData element)
    {
        bool exists = false;
        
        foreach (WeaponInfo weaponInfo in _playerWeapons)
            if (element == weaponInfo.Data)
            {
                exists = true;
                weaponInfo.PickupAmmo(_currentWeapon);
                break;
            }
                
        if(!exists)
        {
            var bulletPool = new List<GameObject>();

            for (int i = 0; i < element.MagazineSize; i++)
            {
                bulletPool.Add(Instantiate(element.BulletPrefab));
                bulletPool[i].SetActive(false);
            }

            var weaponInfo = new WeaponInfo(element, bulletPool);
            _playerWeapons.Add(weaponInfo);
            weaponInfo.AmmoPickupEvent += Weapon.Reloading;
            OnAddElement(element);
        }
    }

    public void NextWeapon()
    {
        if(_playerWeapons != null)
        {
            if (_currentIndex < (_playerWeapons.Count - 1))
            {
                _currentIndex++;
            }
            else
            {
                _currentIndex = 0;
            }
            _currentWeapon = _playerWeapons[_currentIndex];
            OnWeaponChange();
        }
    }

    public void PreviousWeapon()
    {
        if (_playerWeapons != null)
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
            }
            else
            {
                _currentIndex = _playerWeapons.Count - 1;
            }
            _currentWeapon = _playerWeapons[_currentIndex];
            OnWeaponChange();
        }
    }

    public void ChangeWeapon(float mouseWheel)
    {
        if(mouseWheel > 0f)
            NextWeapon();
        if (mouseWheel < 0f)
            PreviousWeapon();
    }
}

public class WeaponInfo
{
    private List<GameObject> _bulletPool;
    public WeaponData Data;
    public int AmmoLeft;
    public int AllAmmo;
    public event Action AmmoPickupEvent;

    public WeaponInfo(WeaponData data, List<GameObject> bulletPool)
    {
        Data = data;
        _bulletPool = bulletPool;
        AmmoLeft = data.MagazineSize;
        AllAmmo = data.StartAmmo;
    }

    public void PickupAmmo(WeaponInfo currentWeapon)
    {
        AllAmmo += Data.MagazineSize;
        if(currentWeapon == this && AmmoLeft == 0)
            AmmoPickupEvent?.Invoke();
    }
    
    public void AddAmmo()
    {
        int diff = Data.MagazineSize - AmmoLeft;
        if(AllAmmo - diff >= 0)
        {
            AllAmmo -= diff;
            AmmoLeft += diff;
        }
        else
        {
            AmmoLeft = AllAmmo;
            AllAmmo = 0;
        }
    }

    public void CreateBullet(Transform weapon)
    {
        if(AmmoLeft > 0)
        foreach(GameObject bullet in _bulletPool)
            if(!bullet.activeSelf)
            {
                bullet.transform.position = weapon.position;
                bullet.transform.rotation = weapon.rotation;
                bullet.SetActive(true);
                AmmoLeft--;
                return;
            }
    }
}
