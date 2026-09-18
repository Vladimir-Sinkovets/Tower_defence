using Assets.Game.Scripts.Battle;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class BattleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BattleEntryPoint>();
        }
    }
}