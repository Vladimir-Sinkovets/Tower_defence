using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.UI;
using Assets.Game.Scripts.UI.Windows;
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
            Container.Bind<MetaCurrencyService>().AsSingle();
            Container.Bind<GameStatistics>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<CastleFactory>().AsSingle();
            Container.Bind<HudFactory>().AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
            
            Container.BindInstance(_planeCenter).WhenInjectedInto<PointSelector>();
            Container.BindInterfacesAndSelfTo<PointSelector>().AsSingle();
        }

        private void BindGameManagers()
        {
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameplayOrchestrator>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameOverManager>().AsSingle();
            Container.Bind<EnemyWavesSpawner>().AsSingle();
            
            Container.BindInstance(_perimeterPoints).WhenInjectedInto<EnemyWavesSpawner>();
            Container.BindInterfacesAndSelfTo<WavesController>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingService>().AsSingle();
            Container.BindInstance(_fieldStartupAnimation).AsSingle();
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
            Container.Bind<PointerRouter>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<WindowsManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<WindowFactory>().AsSingle();
            Container.BindInstance(_windowViewsConfig);
            Container.BindInstance(_hudPrefab);
        }
    }
}