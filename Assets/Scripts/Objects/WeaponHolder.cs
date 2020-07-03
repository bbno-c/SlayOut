using Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private List<WeaponInfo> _playerWeapons;

    private WeaponInfo _currentWeapon;
    private int _currentIndex;

    public event Action<WeaponInfo> WeaponChange;

    private void OnWeaponChange()
    {
        WeaponChange?.Invoke(_currentWeapon);
    }

    public void AddElement(WeaponData element)
    {
        if (_playerWeapons == null)
        { 
            _playerWeapons = new List<WeaponInfo>();
            var weaponInfo = new WeaponInfo(element);
            _playerWeapons.Add(weaponInfo);
            return;
        }

        foreach (WeaponInfo weaponInfo in _playerWeapons)
            if (element == weaponInfo.Data)
                weaponInfo.PickupAmmo();
    }

}

public class WeaponInfo
{
    public WeaponData Data;
    public int AmmoLeft;
    public int AllAmmo;

    public WeaponInfo(WeaponData data)
    {
        AmmoLeft = data.MagazineSize;
        AllAmmo = data.StartAmmo;
    }

    private void PickupAmmo()
    {
        AllAmmo += Data.StartAmmo;
    }
    
    private void AddAmmo()
    {
        int diff = data.MagazineSize - AmmoLeft;
        AllAmmo -= diff;
        AmmoLeft += diff;
    }
}
