using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Buildings.Implementations;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Enemies.Implementations;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.Services.Analytics;
using Assets.Game.Scripts.Services.CastleFactories;
using Assets.Game.Scripts.Services.CurrencyBanks;
using Assets.Game.Scripts.Services.EnemyAccessors;
using Assets.Game.Scripts.Services.GameOverManagers;
using Assets.Game.Scripts.Services.GameplayOrchestrators;
using Assets.Game.Scripts.Services.GameResultSavers;
using Assets.Game.Scripts.Services.GameStoppers;
using Assets.Game.Scripts.Services.HudFactories;
using Assets.Game.Scripts.Services.PointerRouters;
using Assets.Game.Scripts.Services.Registries;
using Assets.Game.Scripts.Services.RewardCalculators;
using Assets.Game.Scripts.Services.SceneLoaders;
using Assets.Game.Scripts.Services.Statistics;
using Assets.Game.Scripts.UI;
using Assets.Game.Scripts.UI.Windows;
using Assets.Game.Scripts.Upgrades.Implementations;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private WavesConfig _wavesConfig;
        [SerializeField] private BuildingsConfig _buildingsConfig;
        [SerializeField] private MetaCurrencyConfig _metaCurrencyConfig;
        [SerializeField] private FieldStartupAnimation _fieldStartupAnimation;
        [SerializeField] private HUD _hudPrefab;
        [SerializeField] private Transform[] _perimeterPoints;
        [SerializeField] private Transform _planeCenter;
        [SerializeField] private WindowViewsConfig _windowViewsConfig;
        
        public override void InstallBindings()
        {
            BindServices();
            BindInput();
            BindGameManagers();
            BindRegisters();
            BindConfigs();
            BindUI();
        }

        private void BindServices()
        {
            Container.Bind<CurrencyBank>().AsSingle();
            Container.Bind<GameStatistics>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<CastleFactory>().AsSingle();
            Container.BindInterfacesTo<BuildingUpgradeApplier>().AsSingle();
            Container.BindInterfacesAndSelfTo<HudFactory>().AsSingle();
            Container.BindInterfacesTo<EnemyAccessor>().AsSingle();
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesTo<BuildingFactory>().AsSingle();
            Container.BindInterfacesTo<ProjectileFactory>().AsSingle();
            Container.BindInterfacesTo<VFXFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<Analytics>().AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();

            Container.BindInstance(_planeCenter).AsSingle();
            Container.BindInterfacesAndSelfTo<PointSelector>().AsSingle();
        }

        private void BindGameManagers()
        {
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayOrchestrator>().AsSingle();

            Container.BindInstance(_perimeterPoints).AsSingle();
            Container.BindInterfacesTo<EnemyWavesSpawner>().AsSingle();
            
            Container.BindInterfacesTo<WavesController>().AsSingle();
            Container.BindInterfacesTo<BuildingService>().AsSingle();
            Container.BindInstance(_fieldStartupAnimation).AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameOverManager>().AsSingle();
            Container.BindInterfacesTo<GameResultSaver>().AsSingle();
            Container.BindInterfacesTo<GameStopper>().AsSingle();
            Container.BindInterfacesTo<RewardCalculator>().AsSingle();
        }

        private void BindRegisters()
        {
            Container.Bind<Registry<Enemy>>().AsSingle();
            Container.Bind<Registry<Building>>().AsSingle();
        }

        private void BindConfigs()
        {
            Container.BindInstance(_buildingsConfig).AsSingle();
            Container.BindInstance(_metaCurrencyConfig).AsSingle();
            Container.BindInstance(_wavesConfig).AsSingle();
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<PointerRouter>().AsSingle();
            
            Container.BindInterfacesTo<WindowsManager>().AsSingle();
            Container.BindInterfacesTo<WindowFactory>().AsSingle();
            Container.BindInstance(_windowViewsConfig).AsSingle();
            Container.BindInstance(_hudPrefab).AsSingle();
        }
    }
}