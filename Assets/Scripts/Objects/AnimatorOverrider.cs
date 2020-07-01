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
        Animator.runtimeAnimatorController = Skin.GetSkinAnimatorOverrideController(AnimationName.M16);
    }
}
