namespace Assets.Game.Scripts.Common.UniversalStateMachine
{
    public abstract class State
    {
        protected readonly IStateSwitcher StateSwitcher;

        protected State(IStateSwitcher stateSwitcher)
        {
            StateSwitcher = stateSwitcher;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }
}