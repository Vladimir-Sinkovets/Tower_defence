using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public abstract class Building : MonoBehaviour
    {
        private Registry<Building> _buildingRegistry;
        private BuildingFactory _config;

        public float RadiusOfOccupiedSpace => _config.RadiusOfOccupiedSpace;

        [Inject]
        public virtual void Construct(Registry<Building> buildingRegistry) => _buildingRegistry = buildingRegistry;

        public virtual void Init(BuildingFactory config)
        {
            _config = config;
            _buildingRegistry.Register(this);
        }

        public abstract void Stop();

        private void OnDrawGizmos()
        {
            if (_config != null)
                Gizmos.DrawWireSphere(transform.position, RadiusOfOccupiedSpace);
        }


        protected virtual void OnDestroy() => _buildingRegistry.Unregister(this);
    }
}