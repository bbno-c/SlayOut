using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Objects
{
    public abstract class Ability : ScriptableObject
    {
        public Sprite aSprite;
        public AudioClip aSound;
        public float aBaseCoolDown = 1f;

        public abstract void Initialize(GameObject obj);
        public abstract void TriggerAbility();
        public int FindParameterLevel(Parameter parameter, List<AbilityStats> abilityStatsList)
        {
            int level = 0;
            foreach(AbilityStats abilityStats in abilityStatsList)
            {
                if(abilityStats.Ability == this)
                {
                    foreach(AbilityPrameters abilityPrameter in abilityStats.AbilityPrametersList)
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