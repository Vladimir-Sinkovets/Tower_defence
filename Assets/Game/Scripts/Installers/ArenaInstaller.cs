using Assets.Game.Scripts.Arena;
using Assets.Game.Scripts.Arena.Services.EnemyFactories;
using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Assets.Game.Scripts.Arena.Services.PlayerControllers;
using Assets.Game.Scripts.Arena.Services.PlayerFactory;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class ArenaInstaller : MonoInstaller
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private ArenaConfig _arenaConfig;
        [SerializeField] private CinemachineCamera _cineMachineCamera;

        public override void InstallBindings()
        {
            Container.BindInstance(_joystick).AsSingle();
            
            Container.BindInstance(_cineMachineCamera).AsSingle();
            
            Container.BindInstance(_arenaConfig).AsSingle();
            
            Container.BindInterfacesTo<PlayerController>().AsSingle();
            
            Container.BindInterfacesTo<PlayerFactory>().AsSingle();
            
            Container.BindInterfacesTo<ArenaEntryPoint>().AsSingle();
            
            Container.BindInterfacesTo<EnemySpawner>().AsSingle();
            
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
        }
    }
} 