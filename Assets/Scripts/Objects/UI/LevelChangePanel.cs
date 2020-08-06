using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace Objects
{
    public class LevelChangePanel : MonoBehaviour
    {
        public event Action<AbilityInfo, Parameter> LevelUp;
        public event Action<AbilityInfo, Parameter> LevelDown;

        public HorizontalLayoutGroup HorizontalLayoutGroup;
        public TextMeshProUGUI AbilityName;
        public GameObject LevelMarker;

        private GameObject[] _levelMarkers;
        private Parameter _abilityPrameter;
        private AbilityInfo _abilityInfo;

        public void InitPanel(AbilityPrameter abilityPrameter, AbilityInfo abilityInfo)
        {
            _abilityPrameter = abilityPrameter.Parameter;
            _abilityInfo = abilityInfo;

            for (int i = 0; i < abilityPrameter.MaxLevel; i++)
            {
                _levelMarkers[i] = Instantiate(LevelMarker, HorizontalLayoutGroup.transform);
            }
        }

        public void OnLevelUp()
        {
            LevelUp?.Invoke(_abilityInfo, _abilityPrameter);
        }

        public void OnLevelDown()
        {
            LevelDown?.Invoke(_abilityInfo, _abilityPrameter);
        }
    }
}