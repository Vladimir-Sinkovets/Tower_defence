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
        [SerializeField] private WavesController _wavesController;
        [SerializeField] private WavesConfig _wavesConfig;
        [SerializeField] private BuildingService _buildingService;
        [SerializeField] private BuildingsConfig _buildingsConfig;
        [SerializeField] private ChooseBuildingView _chooseBuildingView;
        [SerializeField] private EndGamePanel _endGamePanel;
        [SerializeField] private Castle _castle;
        [SerializeField] private MetaCurrencyConfig _metaCurrencyConfig;
        [SerializeField] private FieldStartupAnimation _fieldStartupAnimation;
        [SerializeField] private PointSelector _pointSelector;

        public override void InstallBindings()
        {
            Container.Bind<Registry<Enemy>>().AsSingle();

            Container.Bind<Registry<Building>>().AsSingle();

            Container.Bind<GameStatistics>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameOverManager>().AsSingle();

            Container.BindInstance(_wavesSpawner).AsSingle();

            Container.BindInstance(_castle).AsSingle();

            Container.BindInstance(_buildingsConfig).AsSingle();

            Container.BindInstance(_metaCurrencyConfig).AsSingle();

            Container.BindInterfacesTo<ChooseBuildingView>().FromInstance(_chooseBuildingView).AsSingle();

            Container.BindInstance(_endGamePanel).AsSingle();

            Container.BindInstance(_fieldStartupAnimation).AsSingle();

            Container.BindInstance(_buildingService).AsSingle();

            Container.BindInstance(_wavesController).AsSingle();

            Container.BindInstance(_wavesConfig).AsSingle();

            Container.Bind<CurrencyBank>().AsSingle();

            Container.Bind<ChooseBuildingPresenter>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();

            Container.Bind<MetaCurrencyService>().AsSingle();

            Container.Bind<SceneLoader>().AsSingle();

            Container.BindInstance(_pointSelector).AsSingle();
        }
    }
}