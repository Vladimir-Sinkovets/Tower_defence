using System;
using Assets.Game.Scripts.Arena.Services.EnemyFactories;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.EnemySpawners
{
    public class EnemySpawner : IEnemySpawner, IInRoomCallbacks, IDisposable, ITickable
    {
        private const string NextSpawnTimeKey = "EnemySpawner_NextSpawnTime";
        
        private readonly IEnemyFactory _enemyFactory;
        private readonly EnemySpawnConfig _enemySpawnConfig;
        private bool _isSpawning;
        
        private double _nextSpawnTime;
        
        public EnemySpawner(IEnemyFactory enemyFactory, EnemySpawnConfig enemySpawnConfig)
        {
            _enemyFactory = enemyFactory;
            _enemySpawnConfig = enemySpawnConfig;
        }

        public void Init()
        {
            _isSpawning = true;
            
            PhotonNetwork.AddCallbackTarget(this);
            
            if (PhotonNetwork.IsMasterClient)
            {
                _nextSpawnTime = (float) PhotonNetwork.Time + _enemySpawnConfig.TimeBetweenSpawns;
                UpdateNextSpawnTimeProperty();
            }
        }

        public void Tick() => HandleSpawn();

        private void HandleSpawn()
        {
            if (!_isSpawning)
                return;

            if (!PhotonNetwork.IsMasterClient)
                return;

            if (_nextSpawnTime <= PhotonNetwork.Time)
            {
                _enemyFactory.Spawn(_enemySpawnConfig.EnemyConfig, Vector3.zero);
                
                _nextSpawnTime = PhotonNetwork.Time + _enemySpawnConfig.TimeBetweenSpawns;
                
                UpdateNextSpawnTimeProperty();
            }
        }

        private double GetNextSpawnValue()
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(NextSpawnTimeKey, out var nextSpawnTime))
                return (double) nextSpawnTime;
            
            return PhotonNetwork.Time + _enemySpawnConfig.TimeBetweenSpawns;
        }

        private void UpdateNextSpawnTimeProperty()
        {
            var props = new Hashtable
            {
                { NextSpawnTimeKey, _nextSpawnTime }
            };
            
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }

        public void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
        {
            if (PhotonNetwork.IsMasterClient)
                _nextSpawnTime = GetNextSpawnValue();
        }
        
        public void Dispose()
        {
            _isSpawning = false;
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        #region
        public void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer) { }
        public void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) { }
        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) { }
        public void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, Hashtable changedProps){ }
        #endregion
    }
}