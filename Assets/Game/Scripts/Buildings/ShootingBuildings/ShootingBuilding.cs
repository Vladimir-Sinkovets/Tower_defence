using System;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Buildings.States;
using Assets.Game.Scripts.Common.UniversalStateMachine;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class ShootingBuilding : Building
    {
        public event Action OnStopped;
        
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private WeaponAnimation _preShootAnimation;
        [SerializeField] private float _searchTargetInterval = 0.2f;

        private IEnemyAccessor _enemyAccessor;

        private StateMachine _stateMachine;
        private ShootingBuildingStateMachineData _data;

        [Inject]
        public void Construct(Registry<Building> buildingRegistry, IEnemyAccessor enemyAccessor)
        {
            base.Construct(buildingRegistry);
            _enemyAccessor = enemyAccessor;
        }

        private void Update() => _stateMachine.Update();

        public void Init(ShootingBuildingFactory config)
        {
            base.Init(config);

            _data = new ShootingBuildingStateMachineData
            {
                SearchTargetInterval = _searchTargetInterval,
                Config = config,
                Transform = transform,
                WeaponRoot = _weaponRoot,
                ProjectileStartPosition = _projectileStartPosition,
                PreShootAnimation = _preShootAnimation,
                ShootingBuilding = this,
            };

            _stateMachine = new StateMachine();
            _stateMachine.AddState(new ShootingBuildingWaitState(_data, _stateMachine, _enemyAccessor));
            _stateMachine.AddState(new ShootingBuildingAttackState(_data, _stateMachine));
            _stateMachine.AddState(new ShootingBuildingStopState(_stateMachine));
            

            _stateMachine.SetStartState<ShootingBuildingWaitState>();
        }

        public override void Stop() => OnStopped?.Invoke();
        
        protected override void OnDestroy()
        {
            base.OnDestroy();

            _stateMachine.Dispose();
        }
    }
}