using Assets.Game.Scripts.Arena.Buildings.Interfaces;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.Buildings.Implementations
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly IInstantiator _instantiator;

        public ProjectileFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public Projectile Create(string projectilePrefabName, ProjectileData data)
        {
            var projectile = PhotonNetwork.Instantiate(projectilePrefabName, data.Position, Quaternion.identity)
                .GetComponent<Projectile>();

            projectile.Init(data.Target, data.Damage, data.ProjectileSpeed, data.ArcHeight, data.HitVFXPrefab);

            return projectile;
        }
    }

    public class ProjectileData
    {
        public Vector3 Position;
        public ArenaEnemy Target;
        public int Damage;
        public float ProjectileSpeed;
        public float ArcHeight;
        public ParticleSystem HitVFXPrefab;
    }
}