using Assets.Game.Scripts.Common.UniversalStateMachine;
using Assets.Game.Scripts.Services.EnemyAccessors;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings.States
{
    public class ShootingBuildingWaitState : State
    {
        private readonly ShootingBuildingStateMachineData _data;
        private readonly IEnemyAccessor _enemyAccessor;
        
        private float _nextSearchTime;

        public ShootingBuildingWaitState(ShootingBuildingStateMachineData data, IStateSwitcher stateSwitcher, IEnemyAccessor enemyAccessor) : base(stateSwitcher)
        {
            _data = data;
            
            _enemyAccessor = enemyAccessor;
        }

        public override void Enter()
        {
            _nextSearchTime = 0;
            
            _data.ShootingBuilding.OnStopped += OnStoppedHandler;
        }


        public override void Exit() => _data.ShootingBuilding.OnStopped -= OnStoppedHandler;

        public override void Update()
        {
            if (!(Time.time >= _nextSearchTime))
                return;
            
            _nextSearchTime = Time.time + _data.SearchTargetInterval;

            FindTarget();
        }
        
        private void OnStoppedHandler() => StateSwitcher.SwitchState<ShootingBuildingStopState>();

        private void FindTarget()
        {
            _data.CurrentTarget = _enemyAccessor.FindNearestEnemy(_data.Transform.position, _data.Settings.AttackRadius);

            if (_data.CurrentTarget == null)
                return;
            
            StateSwitcher.SwitchState<ShootingBuildingAttackState>();
        }
    }
}