using Assets.Game.Scripts.Shared;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Enemies.Factories
{
    [CreateAssetMenu(fileName = "Simple_enemy_factory", menuName = "Enemies/Simple enemy factory")]
    public class SimpleEnemyFactory : EnemyFactory
    {
        public int Damage = 2;
        
        [SerializeField] private SimpleEnemy _prefab;

        public override Damageable Create(Health target, DiContainer container)
        {
            var simpleEnemy = container.InstantiatePrefabForComponent<SimpleEnemy>(_prefab);

            simpleEnemy.Init(target, config: this);

            return simpleEnemy;
        }
    }
}