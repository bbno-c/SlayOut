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

    public struct AbilityPrameters
    {
        public Parameter Parameter;
        public int MaxLevel;
        public int CurrentLevel;
    }
    
    public struct AbilityStats
    {
        public Ability Ability;
        public List<AbilityPrameters> AbilityPrametersList;
    }

    public class AbilityChange
    {
        public List<AbilityStats> AbilityStats;
    }
}