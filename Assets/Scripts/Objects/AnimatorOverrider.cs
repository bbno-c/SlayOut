using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorOverrider : MonoBehaviour
{
    public Skin Skin;
    
    public Animator TorsoAnimator;
    public Animator LegsAnimator;

    private void Start()
    {
        TorsoAnimator.runtimeAnimatorController = Skin.WalkM16;
    }
}
