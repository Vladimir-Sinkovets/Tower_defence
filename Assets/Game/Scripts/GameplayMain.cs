using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Services;
using System.Collections;
using Assets.Game.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts
{
    public class GameplayMain : MonoBehaviour
    {
        private WavesController _wavesController;
        private Castle _castle;
        private GameOverManager _gameOverManager;
        private FieldStartupAnimation _fieldStartupAnimation;
        private BuildingsConfig _buildingsConfig;
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(
            WavesController waveController,
            Castle castle,
            GameOverManager gameOverManager,
            FieldStartupAnimation fieldStartupAnimation,
            BuildingsConfig buildingsConfig,
            IInstantiator instantiator)
        {
            _wavesController = waveController;
            _castle = castle;
            _gameOverManager = gameOverManager;
            _fieldStartupAnimation = fieldStartupAnimation;
            _buildingsConfig = buildingsConfig;
            _instantiator = instantiator;
        }

        private IEnumerator Start()
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

        private void OnDestroy() => _castle.OnHpEnded -= OnCastleHpEndedHandler;
    }
}