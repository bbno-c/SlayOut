﻿using System;
using System.Collections.Generic;
using Controllers;
using Objects;
using UnityEngine;

namespace Views
{
    public class GameView : MonoBehaviour, IGameView
    {
        public GameController GameController;
        public Character PlayerPrefab;
        public Transform PlayerStartPoint;
        public InputController InputController;
        public CameraFollow CameraFollow;

        public GameObject[] GameObjects;

        [SerializeField]
        public MenuView _menuView;
        [SerializeField]
        private HudView _hudView;

        public IHudView HudView => _hudView;
        public IMenuView MenuView => _menuView;

        [SerializeField]
        private List<AbilityInfo> _presetAbilityStatsList;
        public List<AbilityInfo> PresetAbilityStatsList { get => _presetAbilityStatsList; }

        public event Action PlayerDeadEvent;
        public event Action<float> PlayerHealthChangeEvent;

        private void OnEnable()
        {
            GameController.OnOpen(this);
        }

        private void OnDisable()
        {
            GameController.OnClose(this);
        }

        private void Update()
        {
            if (GameController.Player != null)
            {

            }
        }

        public void StartGame()
        {
            SpawnPlayer();
            foreach (var o in GameObjects)
                o.SetActive(true);
        }

        public void StopGame()
        {
            var player = GameController.Player;
            if (player != null && player.Health != null)
            {
                player.Health.DieEvent -= OnPlayerDead;
                player.Health.ChangeEvent -= OnPlayerHealthChange;
            }

            GameController.Player = null;

            foreach (var o in GameObjects)
                o.SetActive(false);
        }

        private void SpawnPlayer()
        {
            var player = Instantiate(PlayerPrefab, PlayerStartPoint.position, PlayerStartPoint.rotation);

            if (player == null)
                return;

            var health = player.Health;

            if (health == null)
                return;

            InputController.SetPlayer(player);
            CameraFollow.Target = player.transform;
            CameraFollow.Character = player;
            GameController.Player = player;

            health.DieEvent += OnPlayerDead;
            health.ChangeEvent += OnPlayerHealthChange;
            OnPlayerHealthChange(health.Hitpoints);

            player.AbilityHolder.SetPlayerAbilities(GameController.PlayerAbilityStats);
        }

        private void OnPlayerDead()
        {
            PlayerDeadEvent?.Invoke();
        }

        private void OnPlayerHealthChange(float value)
        {
            PlayerHealthChangeEvent?.Invoke(value);
        }
    }
}