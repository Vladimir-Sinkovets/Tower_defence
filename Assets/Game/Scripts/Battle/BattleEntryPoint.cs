using Assets.Game.Scripts.Battle.Ecs;
using Zenject;

namespace Assets.Game.Scripts.Battle
{
    public class BattleEntryPoint : IInitializable
    {
        private readonly EcsRunner _ecsRunner;

        public BattleEntryPoint(EcsRunner ecsRunner)
        {
            _ecsRunner = ecsRunner;
        }
        
        public void Initialize()
        {
            _ecsRunner.Init();
        }
    }
}