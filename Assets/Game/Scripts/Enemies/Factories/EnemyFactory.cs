using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Enemies.Factories
{
    public abstract class EnemyFactory : ScriptableObject
    {
        public int Hp = 10;
        public float Speed = 1.0f;
        public float AttackRange = 1.0f;
        public float IntervalBetweenAttacks = 1.0f;
        public int Award = 1;

        public abstract Enemy Create(IInstantiator instantiator);
    }
}