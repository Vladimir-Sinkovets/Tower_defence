using UnityEngine;

namespace Assets.Game.Scripts.Enemy
{
    public class SimpleEnemyFactory : EnemyFactory
    {
        public int Damage = 2;
        
        [SerializeField] private SimpleEnemy _prefab;

        public override Enemy Create()
        {
            var simpleEnemy = Instantiate(_prefab);

            simpleEnemy.Init(null, config: this);

            return simpleEnemy;
        }
    }
}