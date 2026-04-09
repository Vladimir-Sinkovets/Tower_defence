using Assets.Game.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private EnemyWavesSpawner _wavesSpawner;

        public override void InstallBindings()
        {
            Container.Bind<GameContext>().AsSingle();

            Container.Bind<EnemyEvents>().AsSingle();

            Container.BindInstance(_wavesSpawner).AsSingle();

            Container.BindInterfacesAndSelfTo<CurrencyBank>().AsSingle();
        }
    }
}