using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Spawn
{
    public struct EnemySpawner : IComponent
    {
        public float Time;
        public float NextSpawnTime;
        public float TimeBetweenSpawn;
        public GameObject Prefab;
    }
}