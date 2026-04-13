using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Services;
using DG.Tweening;
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

        private Tween _shakeTween;

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
            yield return _fieldStartupAnimation.Play();

            yield return CreateCastleBuilding();

            _castle.Init();

            _buildingController.Init();

            _wavesController.StartWaves();

            _castle.OnHpEnded += OnCastleHpEndedHandler;
            _castle.OnDamaged += OnCastleDamaged;
        }

        private IEnumerator CreateCastleBuilding()
        {
            var building = _buildingsConfig.CastleBuilding.Create(_container);

            building.transform.parent = _castle.transform;
            building.transform.position = _castle.BuildingPosition.transform.position;

            yield return building.transform.PlayFallDownAppearanceAnimation();
        }

        private void OnCastleHpEndedHandler() => _gameOverManager.GameOver();

        private void OnCastleDamaged()
        {
            _shakeTween?.Complete();

            _shakeTween = _castle.transform.DOShakePosition(0.1f, 0.1f, 5);
        }

        private void OnDestroy()
        {
            _castle.OnHpEnded -= OnCastleHpEndedHandler;
            _castle.OnDamaged -= OnCastleDamaged;
        }
    }
}