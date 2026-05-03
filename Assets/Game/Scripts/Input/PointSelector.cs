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
        
        public PointSelector(GameInput input, Transform planeCenter)
        {
            _mainCamera = Camera.main;
            _input = input;
            _planeCenter = planeCenter;
        }

        public Vector3 LastPosition { get; private set; }

        public void Initialize() => _input.Touch += OnTouchHandler;

        private void OnTouchHandler(Vector2 touchPosition)
        {
            if (IsPointOverUI(touchPosition))
                return;

            var position = GetPoint(touchPosition);
            
            LastPosition = position;
            
            OnClicked?.Invoke(position);
        }

        private bool IsPointOverUI(Vector2 position)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = position
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            return results.Count > 0;
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