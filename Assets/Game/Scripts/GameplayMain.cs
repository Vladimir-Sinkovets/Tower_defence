using System;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Services;
using System.Collections;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI;
using Assets.Game.Scripts.UI.Buildings;
using Assets.Game.Scripts.UI.CastleHealth;
using Assets.Game.Scripts.UI.Currency;
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
        private readonly HUD _hudPrefab;
        
        private Health _castleHealth;
        
        private ChooseBuildingPresenter _chooseBuildingPresenter;
        private CurrencyPresenter _currencyPresenter;
        private EndGamePresenter _endGamePresenter;
        private CastleHealthPresenter _castleHealthPresenter;

        public GameplayMain(
            WavesController waveController,
            GameOverManager gameOverManager,
            FieldStartupAnimation fieldStartupAnimation,
            BuildingsConfig buildingsConfig,
            IInstantiator instantiator,
            ICoroutineRunner coroutineRunner,
            HUD hudPrefab)
        {
            _wavesController = waveController;
            _gameOverManager = gameOverManager;
            _fieldStartupAnimation = fieldStartupAnimation;
            _buildingsConfig = buildingsConfig;
            _instantiator = instantiator;
            _coroutineRunner = coroutineRunner;
            _hudPrefab = hudPrefab;
        }

        public void Initialize()
        {
            _coroutineRunner.Run(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return _fieldStartupAnimation.Play();
            
            yield return CreateCastle();

            CreateHUD();

            _wavesController.StartWaves(_castleHealth);
        }

        private void CreateHUD()
        {
            var hud = _instantiator.InstantiatePrefabForComponent<HUD>(_hudPrefab);

            _chooseBuildingPresenter = _instantiator.Instantiate<ChooseBuildingPresenter>(new object[] { hud.ChooseBuildingView });
            _currencyPresenter = _instantiator.Instantiate<CurrencyPresenter>(new object[] { hud.CurrencyView });
            _endGamePresenter = _instantiator.Instantiate<EndGamePresenter>(new object[] { hud.EndGameView });
            _castleHealthPresenter = _instantiator.Instantiate<CastleHealthPresenter>(new object[] { hud.CastleHealthView, _castleHealth });
        }

        private IEnumerator CreateCastle()
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

        public void Dispose()
        {
            _currencyPresenter?.Dispose();
            _chooseBuildingPresenter?.Dispose();
            _endGamePresenter?.Dispose();
            _castleHealthPresenter?.Dispose();

            if (_castleHealth != null) _castleHealth.OnDied -= OnCastleDiedHandler;
        }
    }
}