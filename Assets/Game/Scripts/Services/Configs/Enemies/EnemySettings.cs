using System;

namespace Assets.Game.Scripts.Services.Configs.Enemies
{
    [Serializable]
    public class EnemySettings
    {
        public int Hp = 10;
        public float Speed = 1.0f;
        public float AttackRange = 1.0f;
        public float IntervalBetweenAttacks = 1.0f;
        public int Award = 1;
        public int Damage = 2;
    }
}