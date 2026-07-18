using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Enemies.States;
using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Configs.Enemies;
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
        [SerializeField] private SimpleEnemyView _simpleEnemyView;
        [SerializeField] private HealthBarView _healthBarView;

        private StateMachine _stateMachine;
        private SimpleEnemyStateMachineData _data;

        private Registry<Enemy> _enemyRegistry;
        private CurrencyBank _currencyBank;
        private GameStatistics _gameStatistics;
        private HealthBarPresenter _healthPresenter;
        private IInstantiator _instantiator;

        [Inject]
        public void Construct(Registry<Enemy> enemyRegistry, CurrencyBank currencyBank, GameStatistics gameStatistics, IInstantiator instantiator)
        {
            _enemyRegistry = enemyRegistry;
            _currencyBank = currencyBank;
            _gameStatistics = gameStatistics;
            _instantiator = instantiator;
        }

        public override void Init(EnemySettings settings, Health targetHealth, Transform targetTransform)
        {
            base.Init(settings, targetHealth, targetTransform);

            _data = new SimpleEnemyStateMachineData
            {
                NavMeshAgent = _navMeshAgent,
                Transform = transform,
                View = _simpleEnemyView,
                Settings = settings,
                TargetHealth = targetHealth,
                TargetTransform = targetTransform,
                Enemy = this
            };
            
            SetUpNavMesh();
            SetUpStateMachine();
            SetUpHealthView();

            _enemyRegistry.Register(this);
        }
        
        private void SetUpHealthView()
        {
            _healthPresenter = new HealthBarPresenter(Health, _healthBarView);
            _healthPresenter.Init();
        }

        private void SetUpNavMesh()
        {
            _navMeshAgent.speed = _data.Settings.Speed;
            _navMeshAgent.enabled = true;
            _navMeshAgent.Warp(transform.position);
            _navMeshAgent.ResetPath();
            _navMeshAgent.velocity = Vector3.zero;
        }

        private void SetUpStateMachine()
        {
            _stateMachine = new StateMachine();
            _stateMachine.AddState(_instantiator.Instantiate<SimpleEnemyRunState>(new object[] { _data, _stateMachine }));
            _stateMachine.AddState(_instantiator.Instantiate<SimpleEnemyIdleState>(new object[] { _data, _stateMachine }));
            _stateMachine.AddState(_instantiator.Instantiate<SimpleEnemyAttackState>(new object[] { _data, _stateMachine }));
            _stateMachine.AddState(_instantiator.Instantiate<SimpleEnemyDeathState>(new object[] { _data, _stateMachine }));
            
            _stateMachine.SetStartState<SimpleEnemyRunState>();
        }

        protected override void OnDiedHandler()
        {
            base.OnDiedHandler();
            
            _currencyBank.Add(_data.Settings.Award);
            _gameStatistics.IncreaseKilledEnemyCount();
        }

        private void Update() => _stateMachine?.Update();

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _stateMachine.Dispose();

            _enemyRegistry?.Unregister(this);
            
            _healthPresenter?.Dispose();
        }
    }
}