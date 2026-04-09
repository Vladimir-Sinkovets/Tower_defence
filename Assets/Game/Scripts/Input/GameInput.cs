using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Game.Scripts.Input
{
    public class GameInput : IDisposable
    {
        public event Action<Vector2> Tap;

        private InputActions _inputActions;

        public GameInput()
        {
            _inputActions = new();

            _inputActions.Gameplay.Enable();

            _inputActions.Gameplay.Tap.performed += OnTapPerformedHandler;
        }

        private void OnTapPerformedHandler(InputAction.CallbackContext context)
        {
            Tap?.Invoke(_inputActions.Gameplay.TapPosition.ReadValue<Vector2>());
        }

        public void Dispose()
        {
            _inputActions.Gameplay.Disable();
            _inputActions.Dispose();
        }
    }
}