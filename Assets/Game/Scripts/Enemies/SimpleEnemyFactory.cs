using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    [CreateAssetMenu(fileName = "Simple_enemy_factory", menuName = "Enemies/Simple enemy factory")]
    public class SimpleEnemyFactory : EnemyFactory
    {
        public int Damage = 2;
        
        [SerializeField] private SimpleEnemy _prefab;

        public override Damageable Create(Health target)
        {
            var simpleEnemy = Instantiate(_prefab);

            simpleEnemy.Init(target, config: this);

            return simpleEnemy;
        }
    }
}