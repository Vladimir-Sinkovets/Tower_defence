using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    [CreateAssetMenu(fileName = "Shooting_building_factory", menuName = "Configs/Shooting building factory")]
    public class ShootingBuildingFactory : BuildingFactory
    {
        public float AttackRadius = 4.0f;
        public float AttackInterval = 1.0f;
        public float ProjectileSpeed = 4.0f;
        public int Damage = 1;
        public float RotationSpeed = 360.0f;
        public float ArcHeight = 0.4f;
        public Projectile ProjectilePrefab;
        public ParticleSystem ShootVFX;
        public ParticleSystem HitVFX;

        [SerializeField] private ShootingBuilding _prefab;

        public override Building Create(IInstantiator instantiator)
        {
            var building = instantiator.InstantiatePrefabForComponent<ShootingBuilding>(_prefab);

            building.Init(config: this);

            return building;
        }
    }
}