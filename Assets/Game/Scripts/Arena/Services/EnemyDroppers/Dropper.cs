using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    public class Dropper : IDropper
    {
        private readonly DropConfig _config;

        public Dropper(DropConfig config) => _config = config;

        public void Drop(Vector3 position, ArenaEnemyConfig arenaEnemyConfig)
        {
            var exp = PhotonNetwork.InstantiateRoomObject(_config.ExpPrefabName, position, Quaternion.identity)
                .GetComponent<Experience>();

            exp.Init(_config.Experience);

            for (int i = 0; i < _config.Hp; i++)
            {
                var hp = PhotonNetwork.InstantiateRoomObject(_config.CurrencyPrefabName, position, Quaternion.identity)
                    .GetComponent<Hp>();
                
                hp.Init();
            }
        }
    }
}