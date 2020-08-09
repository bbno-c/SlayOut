using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    [System.Serializable]
    public class AbilityInfoSave
    {
        public bool Checked;
        public string AbilityName;
        public List<AbilityPrameter> AbilityPrametersList;
    }
    [System.Serializable]
    public class AbilityStatsSave
    {
        public List<AbilityInfoSave> abilityStatsSaveList;
    }
}
