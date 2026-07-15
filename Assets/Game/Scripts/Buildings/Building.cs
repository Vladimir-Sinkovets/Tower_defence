using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Services.Configs.Buildings;
using Assets.Game.Scripts.Services.Registries;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        [field: SerializeField] public BuildingAppearanceAnimation AppearanceAnimation { get; private set; } 
        
        private Registry<Building> _buildingRegistry;
        private BuildingSettings _settings;

        public float RadiusOfOccupiedSpace => _settings.RadiusOfOccupiedSpace;

        [Inject]
        public void Construct(Registry<Building> buildingRegistry) => _buildingRegistry = buildingRegistry;

        public virtual void Init(BuildingConfig config, BuildingSettings settings, BuildingType buildingType)
        {
            _settings = settings;
            _buildingRegistry.Register(this);
        }

        private void OnDrawGizmos()
        {
            if (_settings != null)
                Gizmos.DrawWireSphere(transform.position, RadiusOfOccupiedSpace);
        }

        protected virtual void OnDestroy() => _buildingRegistry.Unregister(this);
    }
}