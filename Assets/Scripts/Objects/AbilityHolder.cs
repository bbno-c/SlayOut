using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Objects;

namespace Objects
{
    public interface IAbility
    {
        void ApplyAbility();
    }

    public class AbilityHolder: MonoBehaviour
    {
        private List<IAbility> _playerAbilities;

        public void Apply(int index)
        {
            if(_playerAbilities != null && index+1 <= _playerAbilities.Count && index >= 0)
            {
                _playerAbilities[index].ApplyAbility();
            }
        }
    }
}