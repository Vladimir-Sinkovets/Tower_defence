using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Game.Scripts.Input
{
    public class PointSelector : IDisposable, IInitializable
    {
        public event Action<Vector3> OnClicked;
        
        private readonly Transform _planeCenter;
        private readonly GameInput _input;
        private readonly Camera _mainCamera;
        
        private readonly PointerEventData _cachedEventData;
        private readonly List<RaycastResult> _cachedRaycastResult;

        public bool IsStopped { get; set; }
        
        public PointSelector(GameInput input, Transform planeCenter)
        {
            _mainCamera = Camera.main;
            _input = input;
            _planeCenter = planeCenter;
            
            _cachedEventData = new PointerEventData(EventSystem.current);
            _cachedRaycastResult = new List<RaycastResult>();
        }

        public Vector3 LastPosition { get; private set; }

        public void Initialize() => _input.Touch += OnTouchHandler;

        private void OnTouchHandler(Vector2 touchPosition)
        {
            if (IsStopped)
                return;
            
            if (IsPointOverUI(touchPosition))
                return;

            var position = GetPoint(touchPosition);
            
            LastPosition = position;
            
            OnClicked?.Invoke(position);
        }

        private bool IsPointOverUI(Vector2 position)
        {
            _cachedEventData.position = position;
            _cachedRaycastResult.Clear();

            EventSystem.current.RaycastAll(_cachedEventData, _cachedRaycastResult);

            return _cachedRaycastResult.Count > 0;
        }

        private Vector3 GetPoint(Vector2 touchPosition)
        {
            var ray = _mainCamera.ScreenPointToRay(touchPosition);

            var buildPlane = new Plane(Vector3.up, _planeCenter.position);

            if (buildPlane.Raycast(ray, out var enter))
                return ray.GetPoint(enter);

            return Vector3.zero;
        }

        public void Dispose() => _input.Touch -= OnTouchHandler;
    }
}