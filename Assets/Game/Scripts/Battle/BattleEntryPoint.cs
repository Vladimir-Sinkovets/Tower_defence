using Assets.Game.Scripts.Battle.Ecs;
using Assets.Game.Scripts.Battle.Services.HudFactories;
using Zenject;

namespace Assets.Game.Scripts.Battle
{
    public class BattleEntryPoint : IInitializable
    {
        private readonly EcsRunner _ecsRunner;
        private readonly IHudFactory _hudFactory;

        public BattleEntryPoint(EcsRunner ecsRunner, IHudFactory hudFactory)
        {
            _ecsRunner = ecsRunner;
            _hudFactory = hudFactory;
        }
        
        public void Initialize()
        {
            _hudFactory.CreateHUD();
            
            _ecsRunner.Init();
        }
    }
}