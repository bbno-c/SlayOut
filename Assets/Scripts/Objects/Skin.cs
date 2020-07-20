using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Objects
{
    [CreateAssetMenu(fileName = "NewSkin", menuName = "Skin")]
    public class Skin : ScriptableObject
    {
        [SerializeField] private List<SkinData> _skinData;

        public AnimatorOverrideController this[AnimationName index]
        {
            get
            {
                if (_skinData != null)
                    foreach (SkinData skin in _skinData)
                        if(index == skin.Name)
                            return skin.Controller;
                return null;
            }
        }

        public AnimatorOverrideController GetSkinAnimatorOverrideController(AnimationName index)
        {
            return this[index];
        }
    }

    [System.Serializable]
    class SkinData
    {
        [SerializeField] private AnimationName _name;
        public AnimationName Name
        {
            get { return _name; }
            protected set { }
        }
        [SerializeField] AnimatorOverrideController _controller;
        public AnimatorOverrideController Controller
        {
            get { return _controller; }
            protected set { }
        }
    }

    public enum AnimationName
    {
        Unarmed,
        M16,
        Bat,
        Shotgun
    }
}
