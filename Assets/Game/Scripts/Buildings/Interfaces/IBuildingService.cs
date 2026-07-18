using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Buildings.Interfaces
{
    public interface IBuildingService
    {
        bool IsPositionAvailable(Vector3 position);
        UniTask<bool> TryBuildAsync(BuildingConfig config, Vector3 position);
    }
}