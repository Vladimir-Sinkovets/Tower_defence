using System.Threading.Tasks;

namespace Assets.Game.Scripts.Enemies.Interfaces
{
    public interface IEnemyFactory
    {
        Task<Enemy> Create(EnemyConfig config);
    }
}