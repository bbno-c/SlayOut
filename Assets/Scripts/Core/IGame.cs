using System;
using Objects;

namespace Core
{
    public interface IGame
    {
        public Character Player { get; set; }

        event Action EndGameEvent;
        event Action<int> ScoreChangedEvent;
        event Action<float> PlayerHealthChangeEvent;

        void NewGame();
    }
}