using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Objects;

namespace Objects
{
    public class LevelChangePanel : MonoBehaviour
    {
        public HorizontalLayoutGroup LevelMarkersLayoutGroup;
        public TextMeshProUGUI ParameterName;
        public LevelMarker LevelMarker;

        private LevelMarker[] _levelMarkers;
        private AbilityPrameter _abilityParameter;

        public void InitPanel(AbilityPrameter abilityPrameter)
        {
            _levelMarkers = new LevelMarker [abilityPrameter.MaxLevel];

            _abilityParameter = abilityPrameter;

            ParameterName.text = abilityPrameter.ParameterName;

            for (int i = 0; i < abilityPrameter.MaxLevel; i++)
            {
                _levelMarkers[i] = Instantiate(LevelMarker, LevelMarkersLayoutGroup.transform);

                if(i+1 <= abilityPrameter.CurrentLevel)
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
            _abilityParameter.CurrentLevel++;
            _levelMarkers[_abilityParameter.CurrentLevel - 1].LevelMarkerImage.color = Color.green;
        }

        public void OnLevelDown()
        {
            if(_abilityParameter.CurrentLevel > 0)
            {
                _levelMarkers[_abilityParameter.CurrentLevel - 1].LevelMarkerImage.color = Color.red;
                _abilityParameter.CurrentLevel--;
            }
        }
    }
}