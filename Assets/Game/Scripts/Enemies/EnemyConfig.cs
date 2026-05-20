using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    [CreateAssetMenu(fileName = "Enemy_config", menuName = "Configs/Enemy config")]
    public class EnemyConfig : ScriptableObject
    {
        public int Hp = 10;
        public float Speed = 1.0f;
        public float AttackRange = 1.0f;
        public float IntervalBetweenAttacks = 1.0f;
        public int Award = 1;
        public int Damage = 2;
        public Enemy Prefab;
    }
}