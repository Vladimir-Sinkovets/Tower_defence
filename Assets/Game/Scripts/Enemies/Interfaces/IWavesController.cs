using Assets.Game.Scripts.Shared;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies.Interfaces
{
    public interface IWavesController
    {
        int WavesCount { get; }
        void StartWaves(Health target, Transform targetTransform);
        void Stop();
        void Resume();
    }
}