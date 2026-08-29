using Photon.Pun;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyFactories
{
    public class EnemyFactory : IEnemyFactory
    {
        public void Spawn()
        {
            Debug.Log($"PhotonNetwork.Time - {PhotonNetwork.Time:F2}");
        }
    }
}