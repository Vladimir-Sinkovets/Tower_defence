using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyFactories
{
    public class EnemyFactory : IEnemyFactory
    {
        public ArenaEnemy Spawn(ArenaEnemyConfig enemyConfig, Vector3 position)
        {
            var enemyGameObject = PhotonNetwork.InstantiateRoomObject(enemyConfig.PrefabName, position, Quaternion.identity);

            var enemy = enemyGameObject.GetComponent<ArenaEnemy>();
            
            return enemy;
        }
    }
}