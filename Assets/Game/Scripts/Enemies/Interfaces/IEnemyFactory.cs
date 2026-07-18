using System.Threading.Tasks;

namespace Assets.Game.Scripts.Enemies.Interfaces
{
    public interface IEnemyFactory
    {
        Task<Enemy> CreateAsync(EnemyConfig config);
    }
}