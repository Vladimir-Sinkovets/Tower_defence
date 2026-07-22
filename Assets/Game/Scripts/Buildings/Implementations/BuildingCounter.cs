namespace Assets.Game.Scripts.Buildings.Implementations
{
    public class BuildingCounter
    {
        public int Count { get; private set; }

        public void Increment() => ++Count;
    }
}