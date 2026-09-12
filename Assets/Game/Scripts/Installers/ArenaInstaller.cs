using Assets.Game.Scripts.Arena;
using Assets.Game.Scripts.Arena.Buildings.Implementations;
using Assets.Game.Scripts.Arena.Buildings.States;
using Assets.Game.Scripts.Arena.Player;
using Assets.Game.Scripts.Arena.Services.EnemyAccessors;
using Assets.Game.Scripts.Arena.Services.EnemyDeathHandlers;
using Assets.Game.Scripts.Arena.Services.EnemyDroppers;
using Assets.Game.Scripts.Arena.Services.EnemyFactories;
using Assets.Game.Scripts.Arena.Services.EnemySpawners;
using Assets.Game.Scripts.Arena.Services.Experiences;
using Assets.Game.Scripts.Arena.Services.GameOverManager;
using Assets.Game.Scripts.Arena.Services.GameResultCalculators;
using Assets.Game.Scripts.Arena.Services.GameStatistic;
using Assets.Game.Scripts.Arena.Services.UIFactories;
using Assets.Game.Scripts.Arena.Services.PlayerAccessors;
using Assets.Game.Scripts.Arena.Services.PlayerControllers;
using Assets.Game.Scripts.Arena.Services.PlayerFactory;
using Assets.Game.Scripts.Arena.UI;
using Assets.Game.Scripts.Arena.UI.Experience;
using Assets.Game.Scripts.Arena.UI.Windows;
using Assets.Game.Scripts.Services.GameResultSavers;
using Assets.Game.Scripts.Services.Registries;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class ArenaInstaller : MonoInstaller
    {
        [SerializeField] private ArenaConfig _arenaConfig;
        [SerializeField] private DropConfig _arenaDropConfig;
        [SerializeField] private ArenaUIConfig _arenaUIConfig;
        [SerializeField] private EnemySpawnConfig _enemySpawnConfig;
        [SerializeField] private CinemachineCamera _cineMachineCamera;
        [SerializeField] private WindowViewsConfig _windowViewsConfig;
        [SerializeField] private ArenaUpgradesConfig _arenaUpgradesConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_cineMachineCamera).AsSingle();
            
            Container.BindInstance(_arenaConfig).AsSingle();
            
            Container.BindInstance(_arenaUIConfig).AsSingle();
            
            Container.BindInstance(_enemySpawnConfig).AsSingle();
            
            Container.BindInstance(_arenaDropConfig).AsSingle();
            
            Container.BindInterfacesTo<PlayerController>().AsSingle();
            
            Container.BindInterfacesTo<PlayerFactory>().AsSingle();
            
            Container.BindInterfacesTo<ArenaEntryPoint>().AsSingle();
            
            Container.BindInterfacesTo<EnemySpawner>().AsSingle();
            
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
            
            Container.BindInterfacesTo<UIFactory>().AsSingle();
            
            Container.BindInterfacesTo<WindowFactory>().AsSingle();
            
            Container.BindInterfacesTo<WindowsManager>().AsSingle();
            
            Container.BindInterfacesTo<GameOverManager>().AsSingle();
            
            Container.BindInstance(_windowViewsConfig).AsSingle();
            
            Container.BindInterfacesTo<GameStatistics>().AsSingle();
            
            Container.BindInterfacesTo<GameResultCalculator>().AsSingle();

            Container.BindInterfacesTo<GameResultSaver>().AsSingle();

            Container.Bind<Registry<ArenaEnemy>>().AsSingle();

            Container.Bind<Registry<ArenaPlayer>>().AsSingle();

            Container.BindInterfacesTo<EnemyAccessor>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ShootingExecutor>().AsSingle();
            
            Container.BindInterfacesTo<ProjectileFactory>().AsSingle();
            
            Container.BindInterfacesTo<VFXFactory>().AsSingle();
            
            Container.BindInterfacesTo<BuildingUpgradeApplier>().AsSingle();
            
            Container.BindInterfacesTo<EnemyDeathHandler>().AsSingle();
            
            Container.BindInterfacesTo<PlayerAccessor>().AsSingle();

            Container.BindInterfacesTo<Dropper>().AsSingle();
            
            Container.BindInterfacesTo<ExperienceService>().AsSingle();
            
            Container.BindInstance(_arenaUpgradesConfig).AsSingle();
        }
    }
} 