using Assets.Game.Scripts.Arena.Player;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.Services.PlayerControllers
{
    public class PlayerController : ITickable, IPlayerController
    {
        private readonly ArenaConfig _arenaConfig;
        
        private Joystick _joystick;
        private ArenaPlayer _arenaPlayer;

        public PlayerController(ArenaConfig arenaConfig)
        {
            _arenaConfig = arenaConfig;
        }

        public void Init(Joystick joystick, ArenaPlayer arenaPlayer)
        {
            _joystick = joystick;
            _arenaPlayer = arenaPlayer;
        }


        public void Tick()
        {
            if (_arenaPlayer == null)
                return;
            
            if (_joystick.Horizontal == 0 || _joystick.Vertical == 0)
                return;
            
            var direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);
            
            _arenaPlayer.CharacterController.Move(direction * (_arenaConfig.Speed * Time.deltaTime));
        }
    }
}