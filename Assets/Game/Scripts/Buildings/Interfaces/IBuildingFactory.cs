using Assets.Game.Scripts.Services.Configs;
using Assets.Game.Scripts.Services.Configs.Buildings;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Buildings.Interfaces
{
    public interface IBuildingFactory
    {
        UniTask<Building> Create(BuildingConfig config, BuildingSettings settings, BuildingType buildingType);
    }
}