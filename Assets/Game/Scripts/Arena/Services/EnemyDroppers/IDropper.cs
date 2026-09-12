using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyDroppers
{
    public interface IDropper
    {
        void Drop(Vector3 position, ArenaEnemyConfig arenaEnemyConfig);
    }
}