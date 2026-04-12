using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.UI;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class GameplayMain : MonoBehaviour
    {
        private WavesController _wavesController;
        private BuildingController _buildingController;
        private Castle _castle;
        private GameOverManager _gameOverManager;

        [Inject]
        public void Construct(
            WavesController waveController,
            BuildingController buildingController,
            Castle castle,
            GameOverManager gameOverManager)
        {
            _wavesController = waveController;
            _buildingController = buildingController;
            _castle = castle;
            _gameOverManager = gameOverManager;
        }

        private void Start()
        {
            _wavesController.StartWaves();
            _buildingController.Init();
            _castle.Init();

            _castle.OnCastleHpEnded += OnCastleHpEndedHandler;
        }

        private void OnCastleHpEndedHandler() => _gameOverManager.GameOver();

        private void OnDestroy() => _castle.OnCastleHpEnded -= OnCastleHpEndedHandler;
    }
}