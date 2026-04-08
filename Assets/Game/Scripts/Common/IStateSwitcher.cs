namespace Assets.Game.Scripts.Common
{
    public interface IStateSwitcher
    {
        void SwitchState<StateT>() where StateT : State;
    }
}