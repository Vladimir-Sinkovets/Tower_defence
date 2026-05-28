using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveService>().To<SaveService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<FirebaseSetup>().AsSingle();
        }
    }
}