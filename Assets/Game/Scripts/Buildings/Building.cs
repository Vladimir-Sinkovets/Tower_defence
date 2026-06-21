using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Services.Registries;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        [field: SerializeField] public BuildingAppearanceAnimation AppearanceAnimation { get; private set; } 
        
        private Registry<Building> _buildingRegistry;
        private BuildingConfig _config;

        public float RadiusOfOccupiedSpace => _config.RadiusOfOccupiedSpace;

        [Inject]
        public void Construct(Registry<Building> buildingRegistry) => _buildingRegistry = buildingRegistry;

        public virtual void Init(BuildingConfig config, BuildingType buildingType)
        {
            _config = config;
            _buildingRegistry.Register(this);
        }

        private void OnDrawGizmos()
        {
            if (_config != null)
                Gizmos.DrawWireSphere(transform.position, RadiusOfOccupiedSpace);
        }

        protected virtual void OnDestroy() => _buildingRegistry.Unregister(this);
    }
}