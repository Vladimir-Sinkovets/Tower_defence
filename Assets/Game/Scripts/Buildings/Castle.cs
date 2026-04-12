using Assets.Game.Scripts.Shared;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class Castle : MonoBehaviour
    {
        public event Action OnCastleHpEnded;

        [SerializeField] private Transform _buildingPosition;
        [SerializeField] private Health _health;

        private BuildingsConfig _buildingsConfig;
        private DiContainer _container;

        [Inject]
        public void Construct(BuildingsConfig buildingsConfig, DiContainer container)
        {
            _buildingsConfig = buildingsConfig;
            _container = container;
        }

        public void Init()
        {
            var building = _buildingsConfig.CastleBuilding.Create(_container);

            building.transform.parent = _buildingPosition;
            building.transform.position = _buildingPosition.transform.position;

            _health.OnDied += OnDiedHandler;
        }

        private void OnDiedHandler() => OnCastleHpEnded?.Invoke();
    }
}