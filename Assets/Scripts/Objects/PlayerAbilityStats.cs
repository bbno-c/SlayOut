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
        public int CurrentLevel;
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
        public List<AbilityStats> AbilityStats;
    }
}