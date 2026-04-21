using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Game.Scripts.Input
{
    public class PointSelector : MonoBehaviour
    {
        public event Action<Vector3> OnClicked;
        
        [SerializeField] private Transform _planeCenter;
        
        private GameInput _input;
        private Camera _mainCamera;
            
        [Inject]
        public void Construct(GameInput input)
        {
            _mainCamera = Camera.main;
            _input = input;
        }
        
        private void Awake()
        {
            _input.Touch += OnTouchHandler;
        }
        
        private void OnTouchHandler(Vector2 touchPosition)
        {
            if (IsPointOverUI(touchPosition))
                return;

            var position = GetPoint(touchPosition);
            
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

        private void OnDestroy()
        {
            _input.Touch -= OnTouchHandler;
        }
    }
}