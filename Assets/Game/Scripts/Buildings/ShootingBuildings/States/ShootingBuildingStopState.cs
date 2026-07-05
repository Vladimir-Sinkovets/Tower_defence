using Assets.Game.Scripts.Common.UniversalStateMachine;

namespace Assets.Game.Scripts.Buildings.States
{
    public class ShootingBuildingStopState : State
    {
        private readonly ShootingBuilding _shootingBuilding;
        public ShootingBuildingStopState(IStateSwitcher stateSwitcher, ShootingBuildingStateMachineData data) : base(stateSwitcher) => _shootingBuilding = data.ShootingBuilding;

        public override void Enter() => _shootingBuilding.OnResume += OnResumeHandler;
        public override void Exit() => _shootingBuilding.OnResume -= OnResumeHandler;
        public void OnResumeHandler() => StateSwitcher.SwitchState<ShootingBuildingWaitState>();
    }
}