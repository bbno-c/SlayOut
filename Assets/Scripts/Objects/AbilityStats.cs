using Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public enum Parameter
    {
        BuildingRadius
    }

    [System.Serializable]
    public struct AbilityPrameter
    {
        public Parameter Parameter;
        public int MaxLevel;
        [HideInInspector] public int CurrentLevel;

        public void LevelUp() => CurrentLevel++;
        public void LevelDown() => CurrentLevel--;
    }

    [System.Serializable]
    public struct AbilityInfo
    {
        public Ability Ability;
        public List<AbilityPrameter> AbilityPrametersList;
    }

    [System.Serializable]
    public class AbilityStats
    {
        public List<AbilityInfo> AbilityStatsList;

        private void AbilityChecked(AbilityInfo ability)
        {
            if(AbilityStatsList ==  null)
                return;

            foreach(AbilityInfo AbilityInfo in AbilityStatsList)
            {
                if(AbilityInfo.Ability == ability.Ability)
                {
                    AbilityStatsList.Remove(AbilityInfo);
                    return;
                }
            }
            AbilityStatsList.Add(ability);
        }

        private void AddLevel(AbilityInfo ability, Parameter parameter)
        {
            if(AbilityStatsList ==  null)
                return;

            foreach(AbilityInfo AbilityInfo in AbilityStatsList)
            {
                if(AbilityInfo.Ability == ability.Ability)
                {
                    foreach (AbilityPrameter existingParam in AbilityInfo.AbilityPrametersList)
                    {
                        if (parameter == existingParam.Parameter)
                        {
                            if (existingParam.CurrentLevel < existingParam.MaxLevel)
                                existingParam.LevelUp();
                        }
                    }
                }
            }
        }

        private void RemoveLevel(AbilityInfo ability, Parameter parameter)
        {
            if(AbilityStatsList ==  null)
                return;

            foreach (AbilityInfo AbilityInfo in AbilityStatsList)
            {
                if (AbilityInfo.Ability == ability.Ability)
                {
                    foreach (AbilityPrameter existingParam in AbilityInfo.AbilityPrametersList)
                    {
                        if (parameter == existingParam.Parameter)
                        {
                            if (existingParam.CurrentLevel < existingParam.MaxLevel)
                                existingParam.LevelDown();
                        }
                    }
                }
            }
        }
    }   
}