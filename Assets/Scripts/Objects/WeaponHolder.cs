﻿using Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private List<WeaponInfo> _playerWeapons;
    private List<WeaponInfo> _playerInactiveWeapons;
    private WeaponInfo _currentWeapon;
    private WeaponInfo _previousWeapon;
    public WeaponInfo PreviousWeapon => _previousWeapon;
    private int _currentIndex;

    public Weapon Weapon;
    public AnimatorOverrider AnimatorOverrider;
    public WeaponData StartWeapon;
    public event Action<WeaponInfo> WeaponChange;
    public event Action<WeaponInfo> ElementAdded;

    private void Start()
    {
        _playerWeapons = new List<WeaponInfo>();
        _playerInactiveWeapons = new List<WeaponInfo>();

        WeaponChange += Weapon.WeaponChangeEvent;
        WeaponChange += AnimatorOverrider.ChangeWeaponAnimation;

        AddElement(StartWeapon);
        _currentIndex = 0;
        _previousWeapon = null;
        _currentWeapon = _playerWeapons[_currentIndex];
        OnWeaponChange();
    }

    private void OnWeaponChange()
    {
        WeaponChange?.Invoke(_currentWeapon);
    }
    
    private void OnAddElement(WeaponInfo element)
    {
        ElementAdded?.Invoke(element);
    }

    private void OnDeactivated(WeaponInfo weapon)
    {

    }

    public void AddElement(WeaponData element)
    {
        bool exists = false;
        
        foreach (WeaponInfo weaponInfo in _playerWeapons)
            if (element == weaponInfo.Data)
            {
                exists = true;
                _playerWeapons.Add(weaponInfo);
                _playerWeapons.RemoveAt(_playerWeapons.IndexOf(weaponInfo));
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
            OnAddElement(weaponInfo);
        }
    }

    public void Next()
    {
        if(_playerWeapons != null)
        {
            for(int i = 1; i < _playerWeapons.Count; i++)
            {
                if (_currentIndex < (_playerWeapons.Count - 1))
                    _currentIndex++;
                else
                    _currentIndex = 0;
                    
                if(_playerWeapons[_currentIndex].IsActive)
                    break;
            }
            
            _previousWeapon = _currentWeapon;
            _currentWeapon = _playerWeapons[_currentIndex];
            OnWeaponChange();
        }
    }

    public void Previous()
    {
        if (_playerWeapons != null)
        {
            for(int i = 1; i < _playerWeapons.Count; i++)
            {
                if (_currentIndex > 0)
                    _currentIndex--;
                else
                    _currentIndex = _playerWeapons.Count - 1;
                    
                if(_playerWeapons[_currentIndex].IsActive)
                    break;
            }
            
            _previousWeapon = _currentWeapon;
            _currentWeapon = _playerWeapons[_currentIndex];
            OnWeaponChange();
        }
    }

    public void ChangeWeapon(float mouseWheel)
    {
        if(mouseWheel > 0f)
            Next();
        if (mouseWheel < 0f)
            Previous();
    }
}

public class WeaponInfo
{
    private List<GameObject> _bulletPool;
    public WeaponData Data;
    public int AmmoLeft;
    public int AllAmmo;
    public event Action<WeaponInfo> AmmoPickupEvent;
    public event Action<WeaponInfo>  AddAmmoEvent;
    public event Action<WeaponInfo>  CreateBulletEvent;
    public event Action<WeaponInfo>  DeactivatedEvent;
    public bool IsActive => AmmoLeft + AllAmmo > 0;

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
        AmmoPickupEvent?.Invoke(this);
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
        AddAmmoEvent?.Invoke(this);
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
                CreateBulletEvent?.Invoke(this);

                if(!IsActive)
                    DeactivatedEvent?.Invoke(this);

                return;
            }
    }
}
