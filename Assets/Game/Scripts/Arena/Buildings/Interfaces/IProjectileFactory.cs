using Assets.Game.Scripts.Arena.Buildings.Implementations;

namespace Assets.Game.Scripts.Arena.Buildings.Interfaces
{
    public interface IProjectileFactory
    {
        Projectile Create(string projectilePrefabName, ProjectileData data);
    }
}