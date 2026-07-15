using UnityEngine;

namespace Assets.Game.Scripts.Buildings.Interfaces
{
    public interface IBuildingService
    {
        bool IsPositionAvailable(Vector3 position);
        bool TryBuild(BuildingConfig config, Vector3 position);
    }
}