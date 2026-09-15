using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Game.Scripts.Arena.Player;
using Assets.Game.Scripts.Services.Registries;

namespace Assets.Game.Scripts.Arena.Services.SpawnPointFinders
{
    public class SpawnPointFinder : ISpawnPointFinder
    {
        private readonly Registry<ArenaPlayer> _playerRegistry;
        
        private const int MaxAttempts = 20;
        
        public SpawnPointFinder(Registry<ArenaPlayer> playerRegistry) => _playerRegistry = playerRegistry;

        public Vector3 FindSpawnPoint(float minSpawnDistance, float maxSpawnDistance, float minDistanceFromPlayers)
        {
            var players = _playerRegistry.All.ToList();
            
            for (int attempt = 0; attempt < MaxAttempts; attempt++)
            {
                var player = players[Random.Range(0, players.Count)];

                var direction = Random.insideUnitCircle.normalized;
                var distance = Random.Range(
                    minSpawnDistance,
                    maxSpawnDistance);

                var spawnPoint = player.transform.position + new Vector3(
                    direction.x,
                    0f,
                    direction.y) * distance;

                if (IsValid(spawnPoint, players, minDistanceFromPlayers))
                    return spawnPoint;
            }

            return Vector3.zero;
        }
        

        private bool IsValid(Vector3 spawnPoint, IEnumerable<ArenaPlayer> players, float minDistanceFromPlayers)
        {
            var minDistanceSqr = minDistanceFromPlayers * minDistanceFromPlayers;

            foreach (var player in players)
            {
                var offset = spawnPoint - player.transform.position;
                offset.y = 0f;

                if (offset.sqrMagnitude < minDistanceSqr)
                    return false;
            }

            return true;
        }
    }
}