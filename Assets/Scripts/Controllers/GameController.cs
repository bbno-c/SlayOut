using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
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

        List<AbilityInfo> PresetAbilityStatsList { get; }
    }

    [CreateAssetMenu(menuName = "Game Controller")]
    public class GameController : ScriptableObject, IGame
    {
        public event Action EndGameEvent;
        public event Action<int> ScoreChangedEvent;
        public event Action<float> PlayerHealthChangeEvent;
        
        public Character Player { get; set; }
        public AbilityStats PlayerAbilityStats { get; set; }

        private readonly List<GameObject> _objects = new List<GameObject>();
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

            PlayerAbilityStats.AbilityStatsList = view.PresetAbilityStatsList;
            LoadAbilityStats();
        }

        public void OnClose(IGameView view)
        {
            SaveAbilityStats();

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

        private void LoadAbilityStats()
        {
            if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                
                AbilityStatsSave abilityStatsSave = new AbilityStatsSave();
                abilityStatsSave = (AbilityStatsSave)bf.Deserialize(file);

                if (abilityStatsSave != null)
                    foreach (AbilityInfoSave abilityInfoSave in abilityStatsSave.abilityStatsSaveList)
                    {
                        AbilityInfo abilityInfo = PlayerAbilityStats?.AbilityStatsList?.Find(x => x.Ability.Name == abilityInfoSave.AbilityName);
                        abilityInfo.Checked = abilityInfoSave.Checked;
                        abilityInfo.AbilityPrametersList = abilityInfoSave.AbilityPrametersList;
                    }

                file.Close();
            }
        }

        private void SaveAbilityStats()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");

            AbilityStatsSave abilityStatsSave = new AbilityStatsSave();
            abilityStatsSave.abilityStatsSaveList = new List<AbilityInfoSave>();

            foreach (AbilityInfo abilityInfo in PlayerAbilityStats.AbilityStatsList)
            {
                AbilityInfoSave abilityInfoSave = new AbilityInfoSave();
                abilityInfoSave.AbilityName = abilityInfo.Ability.Name;
                abilityInfoSave.Checked = abilityInfo.Checked;
                abilityInfoSave.AbilityPrametersList = abilityInfo.AbilityPrametersList;
                abilityStatsSave.abilityStatsSaveList.Add(abilityInfoSave);
            }

            bf.Serialize(file, abilityStatsSave);
            file.Close();
        }
	}
}