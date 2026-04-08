using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public class EnemyRunState : State
    {
        private readonly EnemyStateMachineData _data;

        public EnemyRunState(IStateSwitcher stateSwitcher, EnemyStateMachineData data) : base(stateSwitcher)
        {
            _data = data;
        }

        public override void Enter()
        {
            _data.NavMeshAgent.SetDestination(_data.Target.transform.position);

            _data.View.PlayWalkAnimation();
        }

        public override void Exit()
        {
            _data.NavMeshAgent.isStopped = true;
        }

        public override void Update()
        {
            if (Vector3.Distance(_data.Transform.position, _data.Target.transform.position) <= _data.Config.AttackRange)
            {
                StateSwitcher.SwitchState<EnemyAttackState>();
            }
        }
    }
}