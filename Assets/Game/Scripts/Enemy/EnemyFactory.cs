using UnityEngine;

namespace Assets.Game.Scripts.Enemy
{
    public abstract class EnemyFactory : ScriptableObject
    {
        public int Hp = 10;
        public float Speed = 1.0f;
        public float AttackRange = 1.0f;
        public float IntervalBetweenAttacks = 1.0f;

        public abstract Enemy Create();
    }
}