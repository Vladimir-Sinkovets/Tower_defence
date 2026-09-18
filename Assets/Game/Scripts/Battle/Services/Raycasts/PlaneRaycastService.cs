using UnityEngine;

namespace Assets.Game.Scripts.Battle.Services.Raycasts
{
    public class PlaneRaycastService : IPlaneRaycastService
    {
        private readonly Transform _planeCenter;
        private readonly Camera _mainCamera;

        public PlaneRaycastService(Transform planeCenter, Camera mainCamera)
        {
            _planeCenter = planeCenter;
            _mainCamera = mainCamera;
        }
        
        public bool TryRaycast(Vector2 touchPosition, out Vector3 point)
        {
            var ray = _mainCamera.ScreenPointToRay(touchPosition);

            var buildPlane = new Plane(Vector3.up, _planeCenter.position);

            if (buildPlane.Raycast(ray, out var enter))
            {
                point = ray.GetPoint(enter);
                
                return true;
            }
            
            point = Vector3.zero;
            
            return false;
        }
    }
}