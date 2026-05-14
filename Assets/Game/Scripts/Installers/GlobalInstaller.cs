using Assets.Game.Scripts.Saves;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveService>().To<SaveService>().AsSingle();
        }
    }
}