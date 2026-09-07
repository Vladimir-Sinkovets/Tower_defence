using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;

namespace Assets.Game.Scripts.Arena.ArenaEnemyStates
{
    public class ArenaEnemyChaseState : State
    {
        private readonly ArenaEnemyStateMachineData _data;

        public ArenaEnemyChaseState(IStateSwitcher stateSwitcher, ArenaEnemyStateMachineData data) : base(stateSwitcher)
        {
            _data = data;
        }
        
        public override void Enter()
        {
            if (_data.NavMeshAgent != null && _data.NavMeshAgent.isOnNavMesh)
                _data.NavMeshAgent.isStopped = false;

            _data.NavMeshAgent.speed = _data.Config.Speed;
            
            _data.View.PlayWalkAnimation();

            _data.Enemy.OnDied += OnEnemyDied;
        }

        public override void Exit()
        {
            if (_data.NavMeshAgent != null && _data.NavMeshAgent.isOnNavMesh)
                _data.NavMeshAgent.isStopped = true;

            _data.Enemy.OnDied -= OnEnemyDied;
        }

        public override void Update()
        {
            _data.NavMeshAgent.SetDestination(_data.Target.transform.position);

            if (Vector3.Distance(_data.Enemy.transform.position, _data.Target.transform.position) <= _data.Config.AttackRange)
            {
                StateSwitcher.SwitchState<ArenaEnemyAttackState>();
            }
        }

        private void OnEnemyDied()
        {
            StateSwitcher.SwitchState<ArenaEnemyDeathState>();
        }
    }
}