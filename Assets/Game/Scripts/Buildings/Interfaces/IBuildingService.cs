using UnityEngine;

namespace Assets.Game.Scripts.Buildings.Interfaces
{
    public interface IBuildingService
    {
        bool IsPositionAvailable(Vector3 position);
        bool TryBuild(BuildingOptionConfig optionConfig, Vector3 position);
    }
}