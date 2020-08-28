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
        public string ParameterName;
        private int _maxLevel;
        private int _currentLevel;

        public int CurrentLevel
        {
            get => _currentLevel;
            set { if (value >= 0 && value <= _maxLevel) _currentLevel = value; }
        }
        public int MaxLevel { get => _maxLevel; }

        public void LevelUp() { if (_currentLevel < _maxLevel) CurrentLevel++; }
        public void LevelDown() { if (_currentLevel > 0) CurrentLevel--; }
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