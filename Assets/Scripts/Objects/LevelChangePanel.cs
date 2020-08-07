using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

namespace Objects
{
    public class LevelChangePanel : MonoBehaviour
    {
        public event Action<AbilityInfo, Parameter> LevelUp;
        public event Action<AbilityInfo, Parameter> LevelDown;

        public HorizontalLayoutGroup LevelMarkersLayoutGroup;
        public TextMeshProUGUI ParameterName;
        public LevelMarker LevelMarker;

        private LevelMarker[] _levelMarkers;
        private Parameter _abilityPrameter;
        private AbilityInfo _abilityInfo;
        private int _currentLvl;

        public void InitPanel(AbilityPrameter abilityPrameter, AbilityInfo abilityInfo)
        {
            _abilityInfo = abilityInfo;
            _abilityPrameter = abilityPrameter.Parameter;
            _currentLvl = abilityPrameter.CurrentLevel;

            for (int i = 0; i < abilityPrameter.MaxLevel; i++)
            {
                _levelMarkers[i] = Instantiate(LevelMarker, LevelMarkersLayoutGroup.transform);

                if(i <= _currentLvl)
                {
                    _levelMarkers[i].LevelMarkerImage.color = Color.green;
                }
                else
                {
                    _levelMarkers[i].LevelMarkerImage.color = Color.red;
                }
            }
        }

        public void OnLevelUp()
        {
            
            LevelUp?.Invoke(_abilityInfo, _abilityPrameter);
            _levelMarkers[_currentLvl].LevelMarkerImage.color = Color.green;
            _currentLvl++;
        }

        public void OnLevelDown()
        {
            LevelDown?.Invoke(_abilityInfo, _abilityPrameter);
            _levelMarkers[_currentLvl].LevelMarkerImage.color = Color.red;
            _currentLvl--;
        }
    }
}