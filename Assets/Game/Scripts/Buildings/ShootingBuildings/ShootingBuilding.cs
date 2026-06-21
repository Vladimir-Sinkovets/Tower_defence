using System;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings.States;
using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Shared;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class ShootingBuilding : Building, IStoppable
    {
        public event Action OnStopped;
        
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private WeaponAnimation _preShootAnimation;
        [SerializeField] private float _searchTargetInterval = 0.2f;

        private IInstantiator _instantiator;

        private StateMachine _stateMachine;
        private ShootingBuildingStateMachineData _data;

        [Inject]
        public void Construct(Registry<Building> buildingRegistry, IInstantiator instantiator)
        {
            base.Construct(buildingRegistry);
            _instantiator =  instantiator;
        }

        private void Update() => _stateMachine.Update();

        public override void Init(BuildingConfig config, BuildingType buildingType)
        {
            base.Init(config, buildingType);

            _data = new ShootingBuildingStateMachineData
            {
                SearchTargetInterval = _searchTargetInterval,
                Config = config,
                Transform = transform,
                WeaponRoot = _weaponRoot,
                ProjectileStartPosition = _projectileStartPosition,
                PreShootAnimation = _preShootAnimation,
                ShootingBuilding = this,
                BuildingType = buildingType,
            };

            _stateMachine = new StateMachine();
            _stateMachine.AddState(_instantiator.Instantiate<ShootingBuildingWaitState>(new object[] { _data, _stateMachine }));
            _stateMachine.AddState(_instantiator.Instantiate<ShootingBuildingAttackState>(new object[] { _data, _stateMachine }));
            _stateMachine.AddState(_instantiator.Instantiate<ShootingBuildingStopState>(new object[] { _stateMachine }));
            
            _stateMachine.SetStartState<ShootingBuildingWaitState>();
        }

        public void Stop() => OnStopped?.Invoke();
        
        protected override void OnDestroy()
        {
            base.OnDestroy();

            _stateMachine.Dispose();
        }
    }
}