using Assets.Game.Scripts.Buildings.Interfaces;
using Assets.Game.Scripts.Enemies;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings.Implementations
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IVFXFactory _vfxFactory;

        public ProjectileFactory(IInstantiator instantiator, IVFXFactory vfxFactory)
        {
            _instantiator = instantiator;
            _vfxFactory = vfxFactory;
        }

        public Projectile Create(Projectile projectilePrefab, ProjectileData data)
        {
            var projectile = _instantiator.InstantiatePrefabForComponent<Projectile>(projectilePrefab);

            projectile.transform.position = data.Position;

            projectile.Init(data.Target, data.Damage, data.ProjectileSpeed, data.ArcHeight);

            if (data.HitVFXPrefab != null)
            {
                projectile.OnHit += (position) =>
                {
                    _vfxFactory.Create(data.HitVFXPrefab, position);
                };
            }

            return projectile;
        }
    }

    public class ProjectileData
    {
        public Vector3 Position;
        public Enemy Target;
        public int Damage;
        public float ProjectileSpeed;
        public float ArcHeight;
        public ParticleSystem HitVFXPrefab;
    }
}