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
    public struct AbilityPrameters
    {
        public Parameter Parameter;
        public int MaxLevel;
        [HideInInspector] public int CurrentLevel;
    }

    [System.Serializable]
    public struct AbilityStats
    {
        public Ability Ability;
        public List<AbilityPrameters> AbilityPrametersList;
    }

    [System.Serializable]
    public class PlayerAbilityStats
    {
        public List<AbilityStats> AbilityStatsList;

        private void AbilityChecked(AbilityStats ability)
        {
            if(AbilityStatsList ==  null)
                return;

            foreach(AbilityStats abilityStats in AbilityStatsList)
            {
                if(abilityStats.Ability == ability.Ability)
                {
                    AbilityStatsList.Remove(abilityStats);
                    return;
                }
            }
            AbilityStatsList.Add(ability);
        }

        private void AddLevel(AbilityStats ability, Parameter parameter)
        {
            if(AbilityStatsList ==  null)
                return;

            foreach(AbilityStats abilityStats in AbilityStatsList)
            {
                if(abilityStats.Ability == ability.Ability)
                {
                    //foreach(AbilityPrameters existingParam in AbilityPrametersList)
                    //{
                    //    if(parameter == existingParam.Parameter)
                    //    {
                    //        if(existingParam.CurrentLevel < existingParam.MaxLevel)
                    //            existingParam.CurrentLevel++;
                    //    }
                    //}
                }
            }
        }

        private void RemoveLevel(AbilityStats ability)
        {
            if(AbilityStatsList ==  null)
                return;

            foreach(AbilityStats abilityStats in AbilityStatsList)
            {
                if(abilityStats.Ability == ability.Ability)
                {
                    //foreach(AbilityPrameters existingParam in AbilityPrametersList)
                    //{
                    //    if(parameter == existingParam.Parameter)
                    //    {
                    //        if(existingParam.CurrentLevel > 0)
                    //            existingParam.CurrentLevel--;
                    //    }
                    //}
                }
            }
        }
    }   
}