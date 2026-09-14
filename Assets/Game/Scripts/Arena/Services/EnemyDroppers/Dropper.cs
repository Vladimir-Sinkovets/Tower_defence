using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    public class Dropper : IDropper
    {
        public void Drop(Vector3 position, DropConfig dropConfig)
        {
            var exp = PhotonNetwork.InstantiateRoomObject(dropConfig.ExpPrefabName, position, Quaternion.identity)
                .GetComponent<Experience>();

            exp.Init(dropConfig.Experience);

            var hp = PhotonNetwork.InstantiateRoomObject(dropConfig.CurrencyPrefabName, position, Quaternion.identity)
                .GetComponent<Hp>();
            
            hp.Init(dropConfig.Heal);
        }
    }
}