using System;

namespace Core
{
    public interface IGame
    {
        event Action EndGameEvent;
        event Action<int> ScoreChangedEvent;
        event Action<float> PlayerHealthChangeEvent;

        void NewGame();
    }
}