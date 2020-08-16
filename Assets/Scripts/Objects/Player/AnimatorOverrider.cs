using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

public class AnimatorOverrider : MonoBehaviour
{
    public Skin Skin;
    public Animator Animator;

    private void Start()
    {
    }

    public void ChangeWeaponAnimation(WeaponInfo currentWeapon)
    {
        if(currentWeapon != null)
            Animator.runtimeAnimatorController = Skin.GetSkinAnimatorOverrideController(currentWeapon.Data.WeaponAnimationName);
    }
}
