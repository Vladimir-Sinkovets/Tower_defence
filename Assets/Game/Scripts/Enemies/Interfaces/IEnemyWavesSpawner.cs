using System.Threading;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Enemies.Interfaces
{
    public interface IEnemyWavesSpawner
    {
        bool IsSpawning { get; }
        UniTask SpawnWave(int count, Health target, CancellationToken ct);
    }
}