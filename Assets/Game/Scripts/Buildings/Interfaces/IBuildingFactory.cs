namespace Assets.Game.Scripts.Buildings.Interfaces
{
    public interface IBuildingFactory
    {
        Building Create(BuildingConfig config);
    }
}