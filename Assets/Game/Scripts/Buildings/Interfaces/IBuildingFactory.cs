using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Buildings.Interfaces
{
    public interface IBuildingFactory
    {
        UniTask<Building> Create(BuildingConfig config, BuildingType buildingType);
    }
}