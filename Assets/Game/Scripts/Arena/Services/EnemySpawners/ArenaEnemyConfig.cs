using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemySpawners
{
    [CreateAssetMenu(fileName = "ArenaEnemyConfig", menuName = "Arena/Arena enemy config")]
    public class ArenaEnemyConfig : ScriptableObject
    {
        public string PrefabName = "Enemy";
        public float Speed = 5.5f;
        public float AttackRange = 2.0f;
        public float Damage = 10.0f;
        public float IntervalBetweenAttacks = 2.0f;
    }
}