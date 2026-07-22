using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Enemies.Interfaces
{
    public interface IEnemyFactory
    {
        UniTask<Enemy> CreateAsync(EnemyConfig config);
    }
}