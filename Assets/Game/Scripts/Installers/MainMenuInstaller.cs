using Assets.Game.Scripts.Services;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MetaCurrencyService>().AsSingle();
        }
    }
}