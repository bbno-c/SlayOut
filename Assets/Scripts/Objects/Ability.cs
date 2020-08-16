using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Objects
{
    public abstract class Ability : ScriptableObject
    {
        public string Name;
        public Sprite aSprite;
        public AudioClip aSound;
        public float BaseCoolDown = 1f;

        public event Action AbilityApplyEvent;

        public abstract void Initialize(GameObject obj, AbilityStats playerAbilityStats);
        public abstract void TriggerAbility();
    }
}