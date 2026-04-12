using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Enemies.Factories;
using Assets.Game.Scripts.Enemies.States;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Zenject.SpaceFighter;

namespace Assets.Game.Scripts.Enemies
{
    public class SimpleEnemy : Enemy
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyView _enemyView;

        private StateMachine _stateMachine;
        private EnemyStateMachineData _data;

        private EnemyEvents _enemyEvents;
        private Registry<Enemy> _enemyRegistry;
        private bool _isActive;

        public bool IsActive { get => _isActive; }

        [Inject]
        public void Construct(EnemyEvents enemyEvents, Registry<Enemy> enemyRegistry)
        {
            _enemyEvents = enemyEvents;
            _enemyRegistry = enemyRegistry;
        }

        public void Init(SimpleEnemyFactory config)
        {
            _stateMachine = new StateMachine();
            _data = new EnemyStateMachineData();

            _stateMachine.AddState(new EnemyRunState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyIdleState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyAttackState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyDeathState(_stateMachine, _data));

            _navMeshAgent.speed = config.Speed;

            _data.NavMeshAgent = _navMeshAgent;
            _data.Transform = transform;
            _data.View = _enemyView;
            _data.Config = config;
            _data.Enemy = this;

            _navMeshAgent.enabled = true;
            _navMeshAgent.Warp(transform.position);
            _navMeshAgent.ResetPath();
            _navMeshAgent.velocity = Vector3.zero;

            Health.OnDied += OnDiedHandler;

            Health.ResetHealth();

            _enemyRegistry.Register(this);
        }

        public override void Activate(Health target)
        {
            if (_isActive)
                return;

            _data.Target = target;

            _stateMachine.SetStartState<EnemyRunState>();

            _isActive = true;
        }

        private void OnDiedHandler() => _enemyEvents.EnemyDie();

        private void Update() => _stateMachine.Update();

        protected void OnDestroy()
        {
            Health.OnDied -= OnDiedHandler;

            _stateMachine.Dispose();

            _enemyRegistry?.Unregister(this);
        }

        public override void Deactivate() => _isActive = false;
    }
}