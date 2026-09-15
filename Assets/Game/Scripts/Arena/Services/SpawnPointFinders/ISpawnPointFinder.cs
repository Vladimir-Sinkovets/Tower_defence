using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.SpawnPointFinders
{
    public interface ISpawnPointFinder
    {
        Vector3 FindSpawnPoint(float minSpawnDistance, float maxSpawnDistance, float minDistanceFromPlayers);
    }
}