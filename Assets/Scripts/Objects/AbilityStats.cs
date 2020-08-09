using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public enum Parameter
    {
        BuildingRadius
    }

    [System.Serializable]
    public class AbilityPrameter
    {
        public Parameter Parameter;
        public int MaxLevel;
        [HideInInspector] public int CurrentLevel;

        public void LevelUp() => CurrentLevel++;
        public void LevelDown() => CurrentLevel--;
    }

    [System.Serializable]
    public class AbilityInfo
    {
        public bool Checked;
        public Ability Ability;
        public List<AbilityPrameter> AbilityPrametersList;
    }

    [System.Serializable]
    public class AbilityStats
    {
        public List<AbilityInfo> AbilityStatsList;

        public void AbilityChecked(AbilityInfo ability)
        {
            if(AbilityStatsList ==  null)
                return;

            AbilityStatsList.Find(x => x == ability).Checked = !ability.Checked;
        }

        public int FindParameterLevel(Parameter parameter, string abilityName)
        {
            foreach(AbilityInfo abilityInfo in AbilityStatsList)
            {
                if(abilityInfo.Ability.Name == abilityName)
                    return abilityInfo.AbilityPrametersList.Find(x => x.Parameter == parameter).CurrentLevel;
            }
            return 0;
        }

        public void AddLevel(AbilityInfo ability, Parameter parameter)
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

        public void RemoveLevel(AbilityInfo ability, Parameter parameter)
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
                            if (existingParam.CurrentLevel > 0)
                                existingParam.LevelDown();
                        }
                    }
                }
            }
        }
    }   
}