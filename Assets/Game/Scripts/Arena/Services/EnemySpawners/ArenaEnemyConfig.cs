using Assets.Game.Scripts.Arena.Services.EnemyDroppers;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemySpawners
{
    [CreateAssetMenu(fileName = "ArenaEnemyConfig", menuName = "Arena/Arena enemy config")]
    public class ArenaEnemyConfig : ScriptableObject
    {
        public string PrefabName = "Enemy";
        public float Speed = 5.5f;
        public float AttackRange = 2.0f;
        public int Damage = 1;
        public float IntervalBetweenAttacks = 2.0f;
        public int Hp = 4;
        public float RotationSpeed = 360.0f;
        public DropConfig Drop;
    }
}