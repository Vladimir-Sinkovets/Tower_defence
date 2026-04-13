using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Enemies.Factories
{
    [CreateAssetMenu(fileName = "Simple_enemy_factory", menuName = "Configs/Simple enemy factory")]
    public class SimpleEnemyFactory : EnemyFactory
    {
        public int Damage = 2;
        
        [SerializeField] private SimpleEnemy _prefab;

        public override Enemy Create(DiContainer container)
        {
            var simpleEnemy = container.InstantiatePrefabForComponent<SimpleEnemy>(_prefab);

            simpleEnemy.Init(this);

            return simpleEnemy;
        }
    }
}