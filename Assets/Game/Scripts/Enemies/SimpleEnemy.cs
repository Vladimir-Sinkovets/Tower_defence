using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Enemies.States;
using Assets.Game.Scripts.Services.CurrencyBanks;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Services.Statistics;
using Assets.Game.Scripts.Shared;
using Assets.Game.Scripts.UI.HealthBar;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Game.Scripts.Enemies
{
    public class SimpleEnemy : Enemy
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private HealthBarView _healthBarView;

        private StateMachine _stateMachine;
        private EnemyStateMachineData _data;

        private Registry<Enemy> _enemyRegistry;
        private CurrencyBank _currencyBank;
        private GameStatistics _gameStatistics;
        private HealthBarPresenter _healthPresenter;

        public bool IsActive { get; private set; }

        [Inject]
        public void Construct(Registry<Enemy> enemyRegistry, CurrencyBank currencyBank, GameStatistics gameStatistics)
        {
            _enemyRegistry = enemyRegistry;
            _currencyBank = currencyBank;
            _gameStatistics = gameStatistics;
        }

        public override void Init(EnemyConfig config, Health targetHealth, Transform targetTransform)
        {
            base.Init(config, targetHealth, targetTransform);

            _data = new EnemyStateMachineData
            {
                NavMeshAgent = _navMeshAgent,
                Transform = transform,
                View = _enemyView,
                Config = config,
                TargetHealth = targetHealth,
                TargetTransform = targetTransform,
                Enemy = this
            };
            
            SetUpNavMesh(config);
            SetUpStateMachine();
            SetUpHealthView();

            _enemyRegistry.Register(this);
        }
        
        public override void Activate()
        {
            IsActive = true;
        }

        public override void Deactivate()
        {
            IsActive = false;
        }
        
        private void SetUpHealthView()
        {
            _healthPresenter = new HealthBarPresenter(Health, _healthBarView);
            _healthPresenter.Init();
        }

        private void SetUpNavMesh(EnemyConfig config)
        {
            _navMeshAgent.speed = config.Speed;
            _navMeshAgent.enabled = true;
            _navMeshAgent.Warp(transform.position);
            _navMeshAgent.ResetPath();
            _navMeshAgent.velocity = Vector3.zero;
        }

        private void SetUpStateMachine()
        {
            _stateMachine = new StateMachine();
            _stateMachine.AddState(new EnemyRunState(_stateMachine, _data));
            _stateMachine.AddState(new EnemyIdleState(_stateMachine, _data));
            _stateMachine.AddState(new SimpleEnemyAttackState(_stateMachine, _data));
            _stateMachine.AddState(new SimpleEnemyDeathState(_stateMachine, _data));
            
            _stateMachine.SetStartState<EnemyRunState>();
        }


        protected override void OnDiedHandler()
        {
            base.OnDiedHandler();
            
            _currencyBank.Add(_data.Config.Award);
            _gameStatistics.IncreaseKilledEnemyCount();
        }

        private void Update() => _stateMachine?.Update();

        private new void OnDestroy()
        {
            base.OnDestroy();
            
            Health.OnDied -= OnDiedHandler;

            _stateMachine.Dispose();

            _enemyRegistry?.Unregister(this);
            
            _healthPresenter?.Dispose();
        }
    }
}