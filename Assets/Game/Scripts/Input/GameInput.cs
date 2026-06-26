using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Game.Scripts.Input
{
    public class GameInput : IDisposable, IInitializable
    {
        public event Action<Vector2> Touch;

        private readonly InputActions _inputActions = new();

        public void Initialize()
        {
            _inputActions.Gameplay.Enable();

            _inputActions.Gameplay.Touch.performed += OnTouchPerformedHandler;
        }

        private void OnTouchPerformedHandler(InputAction.CallbackContext context)
        {
            Touch?.Invoke(_inputActions.Gameplay.TouchPosition.ReadValue<Vector2>());
        }

        public void Dispose()
        {
            _inputActions.Gameplay.Touch.performed -= OnTouchPerformedHandler;

            _inputActions.Gameplay.Disable();
            _inputActions.Dispose();
        }
    }
}