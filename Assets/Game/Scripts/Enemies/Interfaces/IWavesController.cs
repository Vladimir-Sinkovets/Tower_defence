using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Enemies.Interfaces
{
    public interface IWavesController
    {
        int WavesCount { get; }
        void StartWaves(Health target);
        void Stop();
    }
}