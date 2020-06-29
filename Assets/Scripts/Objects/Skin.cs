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
    }
}
