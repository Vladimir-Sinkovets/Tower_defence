using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Services;
using DG.Tweening;
using System;
using System.Collections;
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
        private FieldStartupAnimation _fieldStartupAnimation;
        private BuildingsConfig _buildingsConfig;
        private IBuildingAnimations _buildingAnimations;
        private DiContainer _container;

        [Inject]
        public void Construct(
            WavesController waveController,
            BuildingController buildingController,
            Castle castle,
            GameOverManager gameOverManager,
            FieldStartupAnimation fieldStartupAnimation,
            BuildingsConfig buildingsConfig,
            IBuildingAnimations buildingAnimations,
            DiContainer container)
        {
            _wavesController = waveController;
            _buildingController = buildingController;
            _castle = castle;
            _gameOverManager = gameOverManager;
            _fieldStartupAnimation = fieldStartupAnimation;
            _buildingsConfig = buildingsConfig;
            _buildingAnimations = buildingAnimations;
            _container = container;
        }

        private IEnumerator Start()
        {
            _buildingController.Init();

            yield return _fieldStartupAnimation.Play();

            yield return CreateCastleBuilding();

            _castle.Init();

            _wavesController.StartWaves();

            _castle.OnCastleHpEnded += OnCastleHpEndedHandler;
        }

        private IEnumerator CreateCastleBuilding()
        {
            var building = _buildingsConfig.CastleBuilding.Create(_container);

            building.transform.parent = _castle.transform;
            building.transform.position = _castle.BuildingPosition.transform.position;

            yield return _buildingAnimations.PlayBuildingAppearanceAnimation(building);
        }

        private void OnCastleHpEndedHandler() => _gameOverManager.GameOver();

        private void OnDestroy() => _castle.OnCastleHpEnded -= OnCastleHpEndedHandler;
    }
}