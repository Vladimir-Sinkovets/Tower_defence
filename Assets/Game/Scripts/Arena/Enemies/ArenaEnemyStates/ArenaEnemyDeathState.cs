using Assets.Game.Scripts.Common.UniversalStateMachine;
using Photon.Pun;

namespace Assets.Game.Scripts.Arena.ArenaEnemyStates
{
    public class ArenaEnemyDeathState : State
    {
        private readonly ArenaEnemyStateMachineData _data;

        public ArenaEnemyDeathState(IStateSwitcher stateSwitcher, ArenaEnemyStateMachineData data) : base(stateSwitcher)
        {
            _data = data;
        }

        public override void Enter()
        {
            PhotonNetwork.Destroy(_data.Enemy.gameObject);
        }
    }
}