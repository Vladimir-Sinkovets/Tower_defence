using Assets.Game.Scripts.Input;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Game.Scripts.Battle.Ecs.Input.Systems
{
    public class ClickInputSystem : ISystem
    {
        public World World { get; set; }
        
        private readonly InputActions _inputActions = new();
        
        private Stash<ClickEvent> _clickStash;

        public void OnAwake()
        {
            _inputActions.Enable();
            _inputActions.Battle.Enable();
            
            _inputActions.Battle.Touch.performed += OnTouchPerformed;

            _clickStash = World.GetStash<ClickEvent>();
        }
        
        private void OnTouchPerformed(InputAction.CallbackContext ctx)
        {
            var entity = World.CreateEntity();

            _clickStash.Set(entity, new ClickEvent()
            {
                ScreenPosition = _inputActions.Gameplay.TouchPosition.ReadValue<Vector2>(),
            });
        }

        public void OnUpdate(float deltaTime) { }

        public void Dispose()
        {
            _inputActions.Battle.Touch.performed -= OnTouchPerformed;
            
            _inputActions.Dispose();
        }
    }
}