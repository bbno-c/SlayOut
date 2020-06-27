using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName ="NewSkin", menuName ="Skin Asset")]
public class Skin : ScriptableObject
{
    public AnimatorOverrideController WalkUnarmed;
    public AnimatorOverrideController WalkM16;
    

}
