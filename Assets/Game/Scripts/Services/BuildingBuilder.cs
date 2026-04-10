
using Assets.Game.Scripts.Buildings;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class BuildingBuilder
    {
        private Vector3 _position;
        private BuildingConfig _buildingConfig;
        private DiContainer _container;

        public BuildingBuilder(DiContainer container) => _container = container;

        public void SetPosition(Vector3 position) => _position = position;

        public void SetBuilding(BuildingConfig buildingConfig) => _buildingConfig = buildingConfig;

        public void Build()
        {
            var building = _container.InstantiatePrefab(_buildingConfig.Prefab);

            building.transform.position = _position;
        }

        public void Clean()
        {
            _position = Vector3.zero;
            _buildingConfig = null;
        }
    }
}