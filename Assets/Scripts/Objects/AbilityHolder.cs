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
        private AbilityStats _playerAbilityStats;
        private List<PlayerAbility> _playerAbilities;

        private void Start()
        {
            if (_playerAbilities == null)
                return;

            foreach (PlayerAbility playerAbility in _playerAbilities)
                playerAbility.Ability.Initialize(gameObject, _playerAbilityStats);
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
                    // кулдаун по колбеку
                }
            }
        }

        public void SetPlayerAbilities(AbilityStats playerAbilityStats)
        {
            _playerAbilities = new List<PlayerAbility>();

            foreach (AbilityInfo abilityInfo in playerAbilityStats.AbilityStatsList)
            {
                if(abilityInfo.Checked)//---------------
                {
                    PlayerAbility ability = new PlayerAbility();
                    ability.Ability = abilityInfo.Ability;
                    _playerAbilities.Add(ability);
                }
            }
        }
    }
}