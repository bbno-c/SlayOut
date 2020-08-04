using System;
using System.Collections.Generic;
using Core;
using Objects;
using UnityEngine;

namespace Controllers
{
	public interface IGameView
	{
		event Action PlayerDeadEvent;
		event Action<float> PlayerHealthChangeEvent;

		void StartGame();
		void StopGame();

        IHudView HudView { get; }
        IMenuView MenuView { get; }
    }

    [CreateAssetMenu(menuName = "Game Controller")]
    public class GameController : ScriptableObject, IGame
    {
        public event Action EndGameEvent;
        public event Action<int> ScoreChangedEvent;
        public event Action<float> PlayerHealthChangeEvent;
        private readonly List<GameObject> _objects = new List<GameObject>();
        public Character Player { get; set; }

        public PlayerAbilityStats PlayerAbilityStats; // ДЕСЕРPИАЛИЗОВАТЬ

        private IGameView _view;
        private int _scores;

        public void AddObject(GameObject obj)
        {
            _objects.Add(obj);
        }

        public void RemoveObject(GameObject obj)
        {
            _objects.Remove(obj);
        }

        public void AddScore(int value)
        {
            _scores += value;
            ScoreChangedEvent?.Invoke(_scores);
        }

        public void NewGame()
        {
            _scores = 0;
            _view?.StartGame();
            _view?.HudView?.Open(new HudController(this));
        }

		public void OnOpen(IGameView view)
		{
            view.PlayerDeadEvent += OnPlayerDead;
            view.PlayerHealthChangeEvent += OnPlayerHealthChange;
            view.MenuView?.Open(new MenuController(this));
            _view = view;
        }

        public void OnClose(IGameView view)
        {
            view.PlayerDeadEvent -= OnPlayerDead;
            view.PlayerHealthChangeEvent -= OnPlayerHealthChange;
            _view = null;
		}

		private void OnPlayerDead()
		{
			_view?.StopGame();

            _view?.HudView.EndGameView?.Open(new EndGameController(this, _scores));

            foreach (var o in _objects.ToArray())
                Destroy(o);
            _objects.Clear();

            EndGameEvent?.Invoke();
		}

		private void OnPlayerHealthChange(float value)
		{
			PlayerHealthChangeEvent?.Invoke(value);
		}
	}
}
