using Assets.Game.Scripts.Arena.Player;
using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using UnityEngine.AI;

namespace Assets.Game.Scripts.Arena.ArenaEnemyStates
{
    public class ArenaEnemyStateMachineData
    {
        public ArenaPlayer Target;
        public int TargetViewId;
        public ArenaEnemy Enemy;
        public ArenaEnemyConfig Config;
        public ArenaEnemyView View;
        public NavMeshAgent NavMeshAgent;
    }
}