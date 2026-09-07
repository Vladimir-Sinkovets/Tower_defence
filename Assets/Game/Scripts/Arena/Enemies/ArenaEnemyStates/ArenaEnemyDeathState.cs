using Assets.Game.Scripts.Common.UniversalStateMachine;

namespace Assets.Game.Scripts.Arena.ArenaEnemyStates
{
    public class ArenaEnemyDeathState : State
    {
        public ArenaEnemyDeathState(IStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
        }
    }
}