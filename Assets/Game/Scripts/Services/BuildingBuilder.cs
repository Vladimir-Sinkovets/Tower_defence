
using Assets.Game.Scripts.Buildings;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class BuildingBuilder
    {
        private readonly DiContainer _container;
        private readonly Registry<Building> _buildingRegistry;

        private BuildingConfig _buildingConfig;
        private Vector3 _position;

        public BuildingBuilder(DiContainer container, Registry<Building> buildingRegistry)
        {
            _container = container;
            _buildingRegistry = buildingRegistry;
        }

        public void SetPosition(Vector3 position) => _position = position;

        public void SetBuilding(BuildingConfig buildingConfig) => _buildingConfig = buildingConfig;

        public void Build()
        {
            var building = _buildingConfig.BuildingFactory.Create(_container);

            building.transform.position = _position;
        }

        public void Clean()
        {
            _position = Vector3.zero;
            _buildingConfig = null;
        }
    }
}