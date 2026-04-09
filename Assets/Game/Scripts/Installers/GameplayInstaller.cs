using Assets.Game.Scripts.Configs;
using Assets.Game.Scripts.Input;
using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private EnemyWavesSpawner _wavesSpawner;
        [SerializeField] private WavesController _wavesController;
        [SerializeField] private WavesConfig _wavesConfig;

        public override void InstallBindings()
        {
            Container.Bind<GameContext>().AsSingle();

            Container.Bind<EnemyEvents>().AsSingle();

            Container.BindInstance(_wavesSpawner).AsSingle();

            Container.BindInstance(_wavesController).AsSingle();

            Container.BindInstance(_wavesConfig).AsSingle();

            Container.BindInterfacesAndSelfTo<CurrencyBank>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();
        }
    }
}