using Assets.Game.Scripts.Animations;
using Assets.Game.Scripts.Buildings;
using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Enemies;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.UI;
using Assets.Game.Scripts.UI.Buildings;
using Assets.Game.Scripts.UI.Currency;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private EnemyWavesSpawner _wavesSpawner;
        [SerializeField] private WavesController _wavesController;
        [SerializeField] private WavesConfig _wavesConfig;
        [SerializeField] private BuildingService _buildingService;
        [SerializeField] private BuildingsConfig _buildingsConfig;
        [SerializeField] private ChooseBuildingView _chooseBuildingView;
        [SerializeField] private EndGameView _endGameView;
        [SerializeField] private Castle _castle;
        [SerializeField] private MetaCurrencyConfig _metaCurrencyConfig;
        [SerializeField] private FieldStartupAnimation _fieldStartupAnimation;
        [SerializeField] private PointSelector _pointSelector;
        [SerializeField] private CurrencyView _currencyView;

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
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
            Container.BindInstance(_pointSelector).AsSingle();
        }

        private void BindGameManagers()
        {
            Container.BindInterfacesAndSelfTo<GameOverManager>().AsSingle();
            Container.BindInstance(_wavesSpawner).AsSingle();
            Container.BindInstance(_wavesController).AsSingle();
            Container.BindInstance(_buildingService).AsSingle();
            Container.BindInstance(_castle).AsSingle();
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
            Container.BindInterfacesAndSelfTo<CurrencyPresenter>().AsSingle().NonLazy();
            Container.BindInstance<ICurrencyView>(_currencyView).AsSingle();
            
            Container.Bind<EndGamePresenter>().AsSingle().NonLazy();
            Container.BindInstance<IEndGameView>(_endGameView).AsSingle();

            Container.Bind<ChooseBuildingPresenter>().AsSingle().NonLazy();
            Container.BindInstance<IChooseBuildingView>(_chooseBuildingView).AsSingle();
        }
    }
}