using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Objects;

public class WeaponHolder: MonoBehaviour
{
    private List<WeaponInfo> _playerWeapons;
    private WeaponInfo _currentWeapon;
    private WeaponInfo _previousWeapon;
    public WeaponInfo PreviousWeapon => _previousWeapon;
    private int _currentIndex;

    public Weapon Weapon;
    public WeaponData StartWeapon;
    public AnimatorOverrider AnimatorOverrider;

    public event Action<WeaponInfo> WeaponChange;
    public event Action<WeaponInfo> ElementAdded;
    public event Action<WeaponInfo> ElementExist;

    private void Start()
    {
        _playerWeapons = new List<WeaponInfo>();

        WeaponChange += Weapon.WeaponChangeEvent;
        WeaponChange += AnimatorOverrider.ChangeWeaponAnimation;

        AddElement(StartWeapon);
        _currentIndex = 0;
        _previousWeapon = null;
        _currentWeapon = _playerWeapons[_currentIndex];
        OnWeaponChange();
    }

    public void AddElement(WeaponData element)
    {
        if(element == null)
            return;

        bool exists = false;
        
        foreach (WeaponInfo weaponInfo in _playerWeapons)
            if (element == weaponInfo.Data)
            {
                exists = true;

                if(!weaponInfo.IsActive)
                {
                    _playerWeapons.RemoveAt(_playerWeapons.IndexOf(weaponInfo));
                    _playerWeapons.Add(weaponInfo);
                }

                ElementExist.Invoke(_currentWeapon);

                break;
            }
                
        if(!exists)
        {
            WeaponInfo weaponInfo = CreateWeaponInfo(element);
            
            if(weaponInfo == null)
                return;

            _playerWeapons.Add(weaponInfo);
            OnAddElement(weaponInfo);
        }
    }

    private WeaponInfo CreateWeaponInfo(WeaponData newElement)
    {
        WeaponInfo weaponInfo;

        if(newElement is RangeWeaponData)
        {
            return weaponInfo = new RangeWeaponInfo((RangeWeaponData)newElement, this);
        }

        return null;
    }

    private void OnWeaponChange()
    {
        WeaponChange?.Invoke(_currentWeapon);
    }
    
    private void OnAddElement(WeaponInfo element)
    {
        ElementAdded?.Invoke(element);
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