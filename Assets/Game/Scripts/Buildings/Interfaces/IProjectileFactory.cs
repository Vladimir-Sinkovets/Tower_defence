using Assets.Game.Scripts.Buildings.Implementations;

namespace Assets.Game.Scripts.Buildings.Interfaces
{
    public interface IProjectileFactory
    {
        Projectile Create(Projectile projectilePrefab, ProjectileData data);
    }
}