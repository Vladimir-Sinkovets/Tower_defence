using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Services
{
    public class BuildingBuilder
    {
        private readonly DiContainer _container;
        private readonly IBuildingAnimations _buildingAnimations;

        private BuildingConfig _buildingConfig;
        private Vector3 _position;

        public BuildingBuilder(DiContainer container, IBuildingAnimations buildingAnimations)
        {
            _container = container;
            _buildingAnimations = buildingAnimations;
        }

        public void SetPosition(Vector3 position) => _position = position;

        public void SetBuilding(BuildingConfig buildingConfig) => _buildingConfig = buildingConfig;

        public IEnumerator Build()
        {
            var building = _buildingConfig.BuildingFactory.Create(_container);

            building.transform.position = _position;

            yield return _buildingAnimations.PlayBuildingAppearanceAnimation(building);
        }

        public void Clean()
        {
            _position = Vector3.zero;
            _buildingConfig = null;
        }
    }
}