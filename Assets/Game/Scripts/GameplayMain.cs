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
        private DiContainer _container;

        [Inject]
        public void Construct(
            WavesController waveController,
            BuildingController buildingController,
            Castle castle,
            GameOverManager gameOverManager,
            FieldStartupAnimation fieldStartupAnimation,
            BuildingsConfig buildingsConfig,
            DiContainer container)
        {
            _wavesController = waveController;
            _buildingController = buildingController;
            _castle = castle;
            _gameOverManager = gameOverManager;
            _fieldStartupAnimation = fieldStartupAnimation;
            _buildingsConfig = buildingsConfig;
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

            var buildingGroundPosition = building.transform.position;

            building.transform.position += new Vector3(0, 10, 0);

            yield return building.transform.DOMove(buildingGroundPosition, 0.5f)
                .SetEase(Ease.InExpo).WaitForCompletion();

            building.transform.DOScaleX(1.2f, 0.15f)
                .SetEase(Ease.OutBack);

            building.transform.DOScaleZ(1.2f, 0.15f)
                .SetEase(Ease.OutBack);

            yield return building.transform.DOScaleY(0.1f, 0.15f)
                .SetEase(Ease.OutBack).WaitForCompletion();

            yield return building.transform.DOScale(1.0f, 0.15f)
                .SetEase(Ease.OutBack).WaitForCompletion();
        }

        private void OnCastleHpEndedHandler() => _gameOverManager.GameOver();

        private void OnDestroy() => _castle.OnCastleHpEnded -= OnCastleHpEndedHandler;
    }
}