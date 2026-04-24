using System;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Services;
using System.Collections;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Shared;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class GameplayMain : IInitializable, IDisposable
    {
        private const string CastleRootObjectName = "Castle"; 
        
        private readonly WavesController _wavesController;
        private readonly GameOverManager _gameOverManager;
        private readonly FieldStartupAnimation _fieldStartupAnimation;
        private readonly BuildingsConfig _buildingsConfig;
        private readonly IInstantiator _instantiator;
        private readonly ICoroutineRunner _coroutineRunner;
        private Health _castleHealth;

        public GameplayMain(
            WavesController waveController,
            GameOverManager gameOverManager,
            FieldStartupAnimation fieldStartupAnimation,
            BuildingsConfig buildingsConfig,
            IInstantiator instantiator,
            ICoroutineRunner coroutineRunner)
        {
            _wavesController = waveController;
            _gameOverManager = gameOverManager;
            _fieldStartupAnimation = fieldStartupAnimation;
            _buildingsConfig = buildingsConfig;
            _instantiator = instantiator;
            _coroutineRunner = coroutineRunner;
        }

        public void Initialize()
        {
            _coroutineRunner.Run(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return _fieldStartupAnimation.Play();
            
            yield return CreateCastleBuilding();

            //CreateHUD();

            _wavesController.StartWaves(_castleHealth);
        }

        private IEnumerator CreateCastleBuilding()
        {
            var castle = new GameObject(CastleRootObjectName);
            
            _castleHealth = castle.AddComponent<Health>();
            _castleHealth.Init(_buildingsConfig.CastleHp);
            
            var shaker = castle.AddComponent<DamageShaker>();
            shaker.Init(_castleHealth);
            
            
            var building = _buildingsConfig.CastleBuilding.Create(_instantiator);

            building.transform.parent = castle.transform;
            building.transform.position = castle.transform.position;

            yield return building.transform.PlayFallDownAppearanceAnimation();

            _castleHealth.OnDied += OnCastleDiedHandler;
        }

        private void OnCastleDiedHandler() => _gameOverManager.GameOver();

        public void Dispose() => _castleHealth.OnDied -= OnCastleDiedHandler;
    }
}