using System;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Services;
using System.Collections;
using Assets.Game.Scripts.Enemies;
using Zenject;

namespace Assets.Game.Scripts
{
    public class GameplayMain : IInitializable, IDisposable
    {
        private readonly WavesController _wavesController;
        private readonly Castle _castle;
        private readonly GameOverManager _gameOverManager;
        private readonly FieldStartupAnimation _fieldStartupAnimation;
        private readonly BuildingsConfig _buildingsConfig;
        private readonly IInstantiator _instantiator;
        private readonly ICoroutineRunner _coroutineRunner;

        public GameplayMain(
            WavesController waveController,
            Castle castle,
            GameOverManager gameOverManager,
            FieldStartupAnimation fieldStartupAnimation,
            BuildingsConfig buildingsConfig,
            IInstantiator instantiator,
            ICoroutineRunner coroutineRunner)
        {
            _wavesController = waveController;
            _castle = castle;
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

            _castle.Init();

            _wavesController.StartWaves();

            _castle.OnHpEnded += OnCastleHpEndedHandler;
        }

        private IEnumerator CreateCastleBuilding()
        {
            var building = _buildingsConfig.CastleBuilding.Create(_instantiator);

            building.transform.parent = _castle.transform;
            building.transform.position = _castle.BuildingPosition.transform.position;

            yield return building.transform.PlayFallDownAppearanceAnimation();
        }

        private void OnCastleHpEndedHandler() => _gameOverManager.GameOver();

        public void Dispose() => _castle.OnHpEnded -= OnCastleHpEndedHandler;
    }
}