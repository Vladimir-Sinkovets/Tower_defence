using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Spawn
{
    public struct EnemySpawner : IComponent
    {
        public float Time;
        public float NextWaveTime;
        public float TimeBetweenWaves;
        public int EnemyCount;
        public GameObject Prefab;
        public int IncreaseCountPerWave;
    }
}