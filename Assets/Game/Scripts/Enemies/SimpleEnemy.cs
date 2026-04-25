using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Enemies.Factories;
using Assets.Game.Scripts.Enemies.States;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Shared;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Enemies
{
    public class SimpleEnemy : Enemy
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyView _enemyView;

        private StateMachine _stateMachine;
        private EnemyStateMachineData _data;

        private Registry<Enemy> _enemyRegistry;
        private CurrencyBank _currencyBank;
        private GameStatistics _gameStatistics;

        public bool IsActive { get; private set; }

        [Inject]
        public void Construct(Registry<Enemy> enemyRegistry, CurrencyBank currencyBank, GameStatistics gameStatistics)
        {
            _enemyRegistry = enemyRegistry;
            _currencyBank = currencyBank;
            _gameStatistics = gameStatistics;
        }

        public void Init(SimpleEnemyFactory config)
        {
            _stateMachine = new StateMachine();
            _data = new EnemyStateMachineData();

            _stateMachine.AddState(new EnemyRunState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyIdleState(_stateMachine, _data));
            _stateMachine.AddState(new SimpleEnemyAttackState(_stateMachine, _data));
            _stateMachine.AddState(new SimpleEnemyDeathState(_stateMachine, _data));

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

            Health.Init(config.Hp);

            _enemyRegistry.Register(this);
        }

        public override void Activate(Health target)
        {
            if (IsActive)
                return;

            _data.Target = target;

            _stateMachine.SetStartState<EnemyRunState>();

            IsActive = true;
        }

        private void OnDiedHandler()
        {
            _currencyBank.Add(_data.Config.Award);
            _gameStatistics.IncreaseKilledEnemyCount();
        }

        private void Update() => _stateMachine.Update();

        protected void OnDestroy()
        {
            Health.OnDied -= OnDiedHandler;

            _stateMachine.Dispose();

            _enemyRegistry?.Unregister(this);
        }

        public override void Deactivate() => IsActive = false;
    }
}