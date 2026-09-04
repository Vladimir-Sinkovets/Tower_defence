using Assets.Game.Scripts.Arena.Services.ArenaContexts;
using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.EnemyFactories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _container;
        
        public EnemyFactory(DiContainer container) => _container = container;

        public ArenaEnemy Spawn(ArenaEnemyConfig enemyConfig, Vector3 position)
        {
            var enemyGameObject = PhotonNetwork.InstantiateRoomObject(enemyConfig.PrefabName, position, Quaternion.identity);

            var enemy = enemyGameObject.GetComponent<ArenaEnemy>();

            _container.InjectGameObject(enemy.gameObject);
            
            enemy.Init(enemyConfig);

            Debug.Log($"PhotonNetwork.Time - {PhotonNetwork.Time:F2}");
            
            return enemy;
        }
    }
}