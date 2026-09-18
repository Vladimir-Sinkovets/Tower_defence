using Assets.Game.Scripts.Input;
using Scellecs.Morpeh;
using UnityEngine;

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
            _inputActions.Gameplay.Enable();

            _clickStash = World.GetStash<ClickEvent>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inputActions.Gameplay.Touch.WasCompletedThisFrame())
            {
                var entity = World.CreateEntity();

                _clickStash.Set(entity, new ClickEvent()
                {
                    ScreenPosition = _inputActions.Gameplay.TouchPosition.ReadValue<Vector2>(),
                });
            }
        }

        public void Dispose()
        {
            
        }
    }
}