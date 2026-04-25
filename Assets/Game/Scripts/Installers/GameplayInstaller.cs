using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private EnemyWavesSpawner _wavesSpawner;
        [SerializeField] private WavesConfig _wavesConfig;
        [SerializeField] private BuildingsConfig _buildingsConfig;
        [SerializeField] private MetaCurrencyConfig _metaCurrencyConfig;
        [SerializeField] private FieldStartupAnimation _fieldStartupAnimation;
        [SerializeField] private PointSelector _pointSelector;
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private HUD _hudPrefab;

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
            Container.Bind<ICoroutineRunner>().FromInstance(_coroutineRunner);
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
            Container.BindInstance(_pointSelector).AsSingle();
        }

        private void BindGameManagers()
        {
            Container.BindInterfacesAndSelfTo<GameplayMain>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameOverManager>().AsSingle();
            Container.BindInstance(_wavesSpawner).AsSingle();
            Container.BindInterfacesAndSelfTo<WavesController>().AsSingle();
            Container.Bind<BuildingService>().AsSingle();
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

        private void BindUI() => Container.BindInstance(_hudPrefab);
    }
}