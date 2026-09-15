using System;
using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Arena.Buildings.States;
using Assets.Game.Scripts.Arena.Player;
using Assets.Game.Scripts.Arena.Services.ConstantUpgradeAppliers;
using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Shared;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.Buildings.ShootingBuildings
{
    public class ShootingBuilding : MonoBehaviour, IStoppable
    {
        public event Action OnStopped;
        public event Action OnResume;
        
        [SerializeField] private Transform _projectileStartPosition;
        [SerializeField] private Transform _weaponRoot;
        [SerializeField] private WeaponAnimation _preShootAnimation;
        [SerializeField] private float _searchTargetInterval = 0.2f;

        private IInstantiator _instantiator;
        private IConstantUpgradeApplier _upgradeApplier;
        private BuildingConfig _config;

        private StateMachine _stateMachine;
        private ShootingBuildingStateMachineData _data;

        [Inject]
        public void Construct(IInstantiator instantiator, ArenaConfig config, IConstantUpgradeApplier upgradeApplier)
        {
            _instantiator =  instantiator;
            _upgradeApplier = upgradeApplier;
            _config = config.BuildingConfig;
        }

        private void Update() => _stateMachine?.Update();
        
        public void Init(ArenaPlayer player)
        {
            _data = new ShootingBuildingStateMachineData
            {
                SearchTargetInterval = _searchTargetInterval,
                Config = _config,
                Transform = transform,
                WeaponRoot = _weaponRoot,
                ProjectileStartPosition = _projectileStartPosition,
                PreShootAnimation = _preShootAnimation,
                ShootingBuilding = this,
                PlayerViewId = player.PhotonView.ViewID,
                Damage = _upgradeApplier.ApplyDamageUpgrade(_config.Damage),
                AttackInterval = _config.AttackInterval,
            };

            SetUpStateMachine();
        }
        
        public void IncreaseAttackSpeed(float coefficient) => 
            _data.AttackInterval = Math.Max(_config.MinAttackInterval, coefficient * _config.AttackInterval);

        public void IncreaseDamage(int additionalDamage) => _data.Damage += additionalDamage;

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

        protected void OnDestroy() => _stateMachine?.Dispose();
    }
}