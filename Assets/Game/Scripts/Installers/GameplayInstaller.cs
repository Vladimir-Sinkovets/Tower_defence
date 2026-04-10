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
        [SerializeField] private BuildingController _buildingController;
        [SerializeField] private BuildingsConfig _buildingsConfig;
        [SerializeField] private ChooseBuildingPanel _chooseBuildingPanel;
        [SerializeField] private Castle _castle;

        public override void InstallBindings()
        {
            Container.Bind<Registry<Enemy>>().AsSingle();

            Container.Bind<Registry<Building>>().AsSingle();

            Container.Bind<EnemyEvents>().AsSingle(); 

            Container.Bind<GameStatictics>().AsSingle();

            Container.Bind<BuildingBuilder>().AsTransient();

            Container.BindInstance(_wavesSpawner).AsSingle();

            Container.BindInstance(_castle).AsSingle();

            Container.BindInstance(_buildingsConfig).AsSingle();

            Container.BindInstance(_chooseBuildingPanel).AsSingle();

            Container.BindInstance(_buildingController).AsSingle();

            Container.BindInstance(_wavesController).AsSingle();

            Container.BindInstance(_wavesConfig).AsSingle();

            Container.BindInterfacesAndSelfTo<CurrencyBank>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
        }
    }
}