using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Services;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Enemies
{
    public class SimpleEnemy : Damageable
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyView _enemyView;

        private StateMachine _stateMachine;
        private EnemyStateMachineData _data;

        private EnemyEvents _enemyEvents;

        [Inject]
        private void Construct(EnemyEvents enemyEvents)
        {
            _enemyEvents = enemyEvents;
        }

        public void Init(Health _target, SimpleEnemyFactory config)
        {
            _stateMachine = new StateMachine();
            _data = new EnemyStateMachineData()
            {
                NavMeshAgent = _navMeshAgent,
                Target = _target,
                Transform = transform,
                View = _enemyView,
                Config = config,
                Damageable = this,
            };

            _navMeshAgent.speed = config.Speed;

            _stateMachine.AddState(new EnemyRunState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyIdleState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyAttackState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyDeathState(_stateMachine, _data));

            _stateMachine.SetStartState<EnemyRunState>();

            OnDied += OnDiedHandler;
        }

        private void OnDiedHandler()
        {
            _enemyEvents.EnemyDie();
        }

        private void Update() => _stateMachine.Update();

        protected override void OnDestroy()
        {
            base.OnDestroy();


            
            _stateMachine.Dispose();
        }
    }
}