using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.PlayerControllers
{
    public class PlayerController : ITickable, IPlayerController
    {
        private readonly ArenaConfig _arenaConfig;
        private readonly Joystick _joystick;
        
        private Player _player;

        public PlayerController(Joystick joystick, ArenaConfig arenaConfig)
        {
            _joystick = joystick;
            _arenaConfig = arenaConfig;
        }

        public void Init(Player player) => _player = player;

        public void Tick()
        {
            if (_player == null)
                return;
            
            if (_joystick.Horizontal == 0 || _joystick.Vertical == 0)
                return;
            
            var direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
            
            _player.CharacterController.Move(direction * (_arenaConfig.Speed * Time.deltaTime));
        }
    }
}