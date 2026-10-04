using Assets.Game.Scripts.Battle.Configs;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Spawn
{
    public struct EnemySpawner : IComponent
    {
        public float Time;
        public float NextWaveTime;
        public float TimeBetweenWaves;
        public int EnemyCount;
        public int IncreaseCountPerWave;
        public UnitConfig Config;
        public int WaveCount;
    }
    public struct Reward : IComponent
    {
        public int Value;
    }

    public struct UnitChosenEvent : IComponent
    {
        public UnitConfig Config;
    }
    
    public struct Tank : IComponent { }
}