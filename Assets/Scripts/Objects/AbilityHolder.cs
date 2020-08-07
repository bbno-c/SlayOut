using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public struct PlayerAbility
    {
        public Ability Ability;
        public List<AbilityInfo> AbilityStatsList;
        public bool IsAvailable => Timer <= 0;
        public float Timer;
        public void TimerStep() { if (Timer > 0f) Timer -= Time.deltaTime; }
        public void SetCooldown() => Timer = Ability.BaseCoolDown;
    }

    public class AbilityHolder : MonoBehaviour
    {
        private List<PlayerAbility> _playerAbilities;

        private void Start()
        {
            if (_playerAbilities == null)
                return;

            foreach (PlayerAbility playerAbility in _playerAbilities)
                playerAbility.Ability.Initialize(gameObject, playerAbility.AbilityStatsList);
        }

        private void Update()
        {
            if(_playerAbilities != null)
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

        public void SetPlayerAbilities(List<AbilityInfo> abilityStatsList)
        {
            _playerAbilities = new List<PlayerAbility>();

            foreach (AbilityInfo abilityInfo in abilityStatsList)
            {
                PlayerAbility ability;
                ability.Ability = abilityInfo.Ability;
                ability.AbilityStatsList = abilityStatsList;
                ability.Timer = default;
                _playerAbilities.Add(ability);
            }
        }
    }
}