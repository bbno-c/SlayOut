using System;
using Objects;

namespace Core
{
    public interface IGame
    {
        Character Player { get; set; }
        AbilityStats PlayerAbilityStats { get; set; }

        event Action EndGameEvent;
        event Action<int> ScoreChangedEvent;
        event Action<float> PlayerHealthChangeEvent;

        void NewGame();
    }
}