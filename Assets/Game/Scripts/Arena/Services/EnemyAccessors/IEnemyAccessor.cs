using UnityEngine;

namespace Assets.Game.Scripts.Arena.Services.EnemyAccessors
{
    public interface IEnemyAccessor
    {
        ArenaEnemy FindNearestEnemy(Vector3 transformPosition, float settingsAttackRadius);
    }
}