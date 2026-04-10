using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField] private Transform _weaponPosition;

        private BuildingConfig _config;
        private Registry<Building> _buildingRegistry;

        public float RadiusOfOccupiedSpace => _config.RadiusOfOccupiedSpace;

        public Transform WeaponPosition { get => _weaponPosition; }

        [Inject]
        private void Construct(Registry<Building> buildingRegistry) => _buildingRegistry = buildingRegistry;

        public void Init(BuildingConfig config)
        {
            _config = config;
            _buildingRegistry.Register(this);
        }

        private void OnDrawGizmos()
        {
            if (_config != null)
                Gizmos.DrawWireSphere(transform.position, RadiusOfOccupiedSpace);
        }

        private void OnDestroy() => _buildingRegistry?.Unregister(this);
    }
}