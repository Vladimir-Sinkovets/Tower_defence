using Assets.Game.Scripts.Arena;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class ArenaInstaller : MonoInstaller
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private ArenaConfig _arenaConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_joystick).AsSingle();
            
            Container.BindInstance(_characterController).AsSingle();
            
            Container.BindInstance(_arenaConfig).AsSingle();
            
            Container.BindInterfacesTo<PlayerController>().AsSingle();
        }
    }
} 