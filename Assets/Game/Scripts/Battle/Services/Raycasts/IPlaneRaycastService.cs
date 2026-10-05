using UnityEngine;

namespace Assets.Game.Scripts.Battle.Services.Raycasts
{
    public interface IPlaneRaycastService
    {
        bool TryRaycast(Vector2 touchPosition, out Vector3 point);
    }
}