using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Objects
{
    public abstract class Ability : ScriptableObject
    {
        public Sprite aSprite;
        public AudioClip aSound;
        public float BaseCoolDown = 1f;

        public abstract void Initialize(GameObject obj, List<AbilityInfo> abilityStatsList);
        public abstract void TriggerAbility();
        public int FindParameterLevel(Parameter parameter, List<AbilityInfo> AbilityStatsList)
        {
            int level = 0;
            foreach(AbilityInfo AbilityInfo in AbilityStatsList)
            {
                if(AbilityInfo.Ability == this)
                {
                    foreach(AbilityPrameter abilityPrameter in AbilityInfo.AbilityPrametersList)
                    {
                        if(abilityPrameter.Parameter == parameter)
                        {
                            return abilityPrameter.CurrentLevel;
                        }
                    }
                }
            }
            return level;
        }
    }
}