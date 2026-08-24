using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena
{
    public class PlayerController : ITickable
    {
        private readonly CharacterController _characterController;
        private readonly ArenaConfig _arenaConfig;
        private readonly Joystick _joystick;

        public PlayerController(Joystick joystick, CharacterController characterController, ArenaConfig arenaConfig)
        {
            _joystick = joystick;
            _characterController = characterController;
            _arenaConfig = arenaConfig;
        }

        public void Tick()
        {
            if (_joystick.Horizontal == 0 || _joystick.Vertical == 0)
                return;
            
            var direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
            
            _characterController.Move(direction * (_arenaConfig.Speed * Time.deltaTime));
        }
    }
}