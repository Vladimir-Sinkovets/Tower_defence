using UnityEngine;

namespace Assets.Game.Scripts.Battle.Configs
{
    [CreateAssetMenu(fileName = "Battle_spawn_borders_config", menuName = "Battle/Battle borders config")]
    public class BattleSpawnBordersConfig : ScriptableObject
    {
        public Rect PlayerSpawnArea = new Rect(-2.25f, -6.0f, 4.5f, 6.0f);
        public Rect EnemySpawnArea = new Rect(-2.25f, 0.0f, 4.5f, 6.0f);
    }
}