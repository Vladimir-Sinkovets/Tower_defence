using Assets.Game.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class BuildingController : MonoBehaviour
    {
        [SerializeField] private Transform _planeCenter;

        private GameInput _input;
        private BuildingService _buildingService;
        private Camera _mainCamera;

        [Inject]
        private void Construct(GameInput input, BuildingService buildingService)
        {
            _mainCamera = Camera.main;
            _input = input;
            _buildingService = buildingService;
        }

        public void Init()
        {
            _input.Tap += OnTapHandler;
        }

        private void OnTapHandler(Vector2 tapPosition)
        {
            var position = GetPoint(tapPosition);

            _buildingService.StartBuilding(position);
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