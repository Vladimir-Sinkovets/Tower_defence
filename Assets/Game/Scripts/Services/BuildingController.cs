using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private Transform _planeCenter;

        private GameInput _input;
        private ChooseBuildingPanel _chooseBuildingPanel;
        private BuildingsConfig _buildingsConfig;
        private BuildingBuilder _builder;
        private Camera _mainCamera;

        private bool _isBuilding;

        [Inject]
        private void Construct(GameInput input, ChooseBuildingPanel chooseBuildingPanel, BuildingsConfig buildingsConfig, BuildingBuilder builder)
        {
            _mainCamera = Camera.main;
            _input = input;

            _chooseBuildingPanel = chooseBuildingPanel;
            _buildingsConfig = buildingsConfig;
            _builder = builder;
        }

        public void Init()
        {
            _input.Tap += OnTapHandler;

            _chooseBuildingPanel.OnOptionChosen += OnOptionChosenHandler;
        }

        private void OnTapHandler(Vector2 tapPosition)
        {
            if (_isBuilding)
                return;

            var position = GetPoint(tapPosition);

            if (!IsPointFreeToBuild(position))
                return;

            _isBuilding = true;

            StartBuilding(position);
        }

        public void StartBuilding(Vector3 position)
        {
            _builder.Clean();

            _builder.SetPosition(position);

            _chooseBuildingPanel.Open(_buildingsConfig.Buildings);
        }

        private void OnOptionChosenHandler(BuildingConfig buildingConfig)
        {
            _builder.SetBuilding(buildingConfig);
            _builder.Build();

            _isBuilding = false;
        }

        private bool IsPointFreeToBuild(Vector3 point)
        {
            return true;
        }

        private Vector3 GetPoint(Vector2 tapPosition)
        {
            var ray = _mainCamera.ScreenPointToRay(tapPosition);

            var buildPlane = new Plane(Vector3.up, _planeCenter.position);

            if (buildPlane.Raycast(ray, out var enter))
            {
                return ray.GetPoint(enter);
            }

            return Vector3.zero;
        }

        private void OnDestroy()
        {
            _input.Tap -= OnTapHandler;
        }
    }
}