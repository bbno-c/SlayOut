using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Objects;

namespace Objects
{
    [Serializable]
    public struct PlayerAbility
    {
        public Ability Ability;
        public float Cooldown;
        public bool IsAvailable => Timer <= 0;
        public float Timer;
        public void TimerStep() { if (Timer > 0f) Timer -= Time.deltaTime; }
        public void SetCooldown() => Timer = Cooldown;
    }

    public class AbilityHolder : MonoBehaviour
    {
        [SerializeField] private List<PlayerAbility> _playerAbilities;

        private void Start()
        {
            foreach (PlayerAbility playerAbility in _playerAbilities)
                playerAbility.Ability.Initialize(gameObject, null);
        }

        private void Update()
        {
            foreach (PlayerAbility playerAbility in _playerAbilities)
            {
                playerAbility.TimerStep();
            }
        }

        public void Apply(int index)
        {
            if (_playerAbilities != null && index + 1 <= _playerAbilities.Count && index >= 0)
            {
                if(_playerAbilities[index].IsAvailable)
                {
                    _playerAbilities[index].Ability.TriggerAbility();
                    _playerAbilities[index].SetCooldown();
                }
            }
        }
    }
}