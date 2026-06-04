using Assets.Game.Scripts.Enemies;
using UnityEngine;

namespace Assets.Game.Scripts.Services.EnemyAccessors
{
    public interface IEnemyAccessor
    {
        Enemy FindNearestEnemy(Vector3 position, float radius);
    }
}