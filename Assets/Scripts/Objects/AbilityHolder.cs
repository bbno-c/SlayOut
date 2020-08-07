using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class PlayerAbility
    {
        public Ability Ability;
        public bool IsAvailable => Timer <= 0;
        public float Timer = default;
        public void TimerStep() { if (Timer > 0f) Timer -= Time.deltaTime; }
        public void SetCooldown() => Timer = Ability.BaseCoolDown;
    }

    public class AbilityHolder : MonoBehaviour
    {
        public List<AbilityInfo> _abilityStatsList;
        private List<PlayerAbility> _playerAbilities;

        private void Start()
        {
            if (_playerAbilities == null)
                return;

            foreach (PlayerAbility playerAbility in _playerAbilities)
                playerAbility.Ability.Initialize(gameObject, _abilityStatsList);
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
                }
            }
        }

        public void SetPlayerAbilities(List<AbilityInfo> abilityStatsList)
        {
            _playerAbilities = new List<PlayerAbility>();

            foreach (AbilityInfo abilityInfo in abilityStatsList)
            {
                PlayerAbility ability = new PlayerAbility();
                ability.Ability = abilityInfo.Ability;
                _abilityStatsList = abilityStatsList;
                _playerAbilities.Add(ability);
            }
        }
    }
}