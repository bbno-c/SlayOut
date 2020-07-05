using Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public WeaponData WeaponData;
    public float SpawnTime;

    public Animator Animator;
    public BoxCollider2D BoxCollider2D;

    public SpriteRenderer Weapon;
    public SpriteRenderer Shadow;

    private float _timer;

    void Start()
    {
        Weapon.sprite = WeaponData.Sprite;
        Shadow.sprite = WeaponData.Sprite;
        _timer = 0;
        SetState(true);
    }

    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _timer = 0;
                SetState(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponHolder weaponHolder = collision.gameObject.GetComponentInParent<WeaponHolder>();
        if (weaponHolder != null)
        {
            weaponHolder.AddElement(WeaponData);
            _timer = SpawnTime;
            SetState(false);
        }
    }

    private void SetState(bool activate)
    {
        Animator.SetBool("IsActive", activate);
        if (activate)
        {
            Weapon.sprite = WeaponData.Sprite;
            Shadow.sprite = WeaponData.Sprite;
            BoxCollider2D.enabled = true;
        }
        else
        {
            Weapon.sprite = null;
            Shadow.sprite = null;
            BoxCollider2D.enabled = false;
        }       
    }
}
