using System.Threading;
using Assets.Game.Scripts.Shared;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies.Interfaces
{
    public interface IEnemyWavesSpawner
    {
        bool IsSpawning { get; }
        UniTask SpawnWave(int count, Health targetHealth, Transform targetTransform, CancellationToken ct);
        void Stop();
        void Resume();
    }
}