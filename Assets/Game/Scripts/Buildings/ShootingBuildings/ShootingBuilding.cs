using System;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings.States;
using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Services.Configs.Buildings;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Shared;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class ShootingBuilding : Building, IStoppable
    {
        public event Action OnStopped;
        public event Action OnResume;
        
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

        public override void Init(BuildingConfig config, BuildingSettings settings, BuildingType buildingType)
        {
            base.Init(config, settings, buildingType);

            _data = new ShootingBuildingStateMachineData
            {
                SearchTargetInterval = _searchTargetInterval,
                Settings = settings,
                Config = config,
                Transform = transform,
                WeaponRoot = _weaponRoot,
                ProjectileStartPosition = _projectileStartPosition,
                PreShootAnimation = _preShootAnimation,
                ShootingBuilding = this,
                BuildingType = buildingType,
            };

            SetUpStateMachine();
        }

        private void SetUpStateMachine()
        {
            _stateMachine = new StateMachine();
            _stateMachine.AddState(_instantiator.Instantiate<ShootingBuildingWaitState>(new object[] { _data, _stateMachine }));
            _stateMachine.AddState(_instantiator.Instantiate<ShootingBuildingAttackState>(new object[] { _data, _stateMachine }));
            _stateMachine.AddState(_instantiator.Instantiate<ShootingBuildingStopState>(new object[] { _data, _stateMachine }));
            
            _stateMachine.SetStartState<ShootingBuildingWaitState>();
        }

        public void Stop() => OnStopped?.Invoke();
        public void Resume() => OnResume?.Invoke();

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _stateMachine.Dispose();
        }
    }
}