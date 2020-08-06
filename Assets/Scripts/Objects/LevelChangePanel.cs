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
        private int _currentLvl;

        public void InitPanel(AbilityPrameter abilityPrameter, AbilityInfo abilityInfo)
        {
            _abilityPrameter = abilityPrameter.Parameter;
            _abilityInfo = abilityInfo;
            _currentLvl = abilityPrameter.CurrentLevel;

            for (int i = 0; i < abilityPrameter.MaxLevel; i++)
            {
                _levelMarkers[i] = Instantiate(LevelMarker, HorizontalLayoutGroup.transform);

                if(i <= _currentLvl)
                {
                    _levelMarkers[i].GetComponent<Image>().color = Color.green;
                }
            }
        }

        public void OnLevelUp()
        {
            
            LevelUp?.Invoke(_abilityInfo, _abilityPrameter);
            _levelMarkers[_currentLvl].GetComponent<Image>().color = Color.green;
            _currentLvl++;
        }

        public void OnLevelDown()
        {
            LevelDown?.Invoke(_abilityInfo, _abilityPrameter);
            _levelMarkers[_currentLvl].GetComponent<Image>().color = Color.red;
            _currentLvl--;
        }
    }
}