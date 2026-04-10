using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    [CreateAssetMenu(fileName = "Shooting_weapon_factory", menuName = "Configs/Shooting weapon factory")]
    public class ShootingWeaponFactory : WeaponFactory
    {
        public float AttackRadius = 4.0f;
        public float AttackInterval = 1.0f;
        public float ProjectileSpeed = 4.0f;
        public int Damage = 1;
        public float RotationSpeed = 360.0f;
        public float ArcHeight = 0.4f;
        public Projectile ProjectilePrefab;

        [SerializeField] private ShootingWeapon _prefab;

        public override Weapon Create(DiContainer container)
        {
            var weapon = container.InstantiatePrefabForComponent<ShootingWeapon>(_prefab);

            weapon.Init(config: this);

            return weapon;
        }
    }
}