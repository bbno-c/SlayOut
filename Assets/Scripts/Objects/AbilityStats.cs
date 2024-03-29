﻿using UnityEngine;
using System.Collections.Generic;

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
        public string ParameterName;
        [SerializeField] private int _maxLevel;
        [SerializeField] private int _currentLevel;

        public int MaxLevel { get => _maxLevel; }
        public int CurrentLevel
        {
            get => _currentLevel;
            set { if (value >= 0 && value <= _maxLevel) _currentLevel = value; }
        }
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
        public int UsedAbilityPoints;
        public List<AbilityInfo> AbilityStatsList;

        public int FindParameterLevel(Parameter parameter, string abilityName)
        {
            foreach(AbilityInfo abilityInfo in AbilityStatsList)
            {
                if(abilityInfo.Ability.Name == abilityName)
                    return abilityInfo.AbilityPrametersList.Find(x => x.Parameter == parameter).CurrentLevel;
            }
            return 0;
        }
    }   
}