using Assets.Game.Scripts.Battle.Ecs.AI.Systems;
using Assets.Game.Scripts.Battle.Ecs.Damage.Systems;
using Assets.Game.Scripts.Battle.Ecs.Input.Systems;
using Assets.Game.Scripts.Battle.Ecs.Movement.Systems;
using Assets.Game.Scripts.Battle.Ecs.Spawn.Systems;
using Assets.Game.Scripts.Battle.Ecs.Test;
using Assets.Game.Scripts.Battle.Ecs.Unity.Systems;
using Scellecs.Morpeh;
using Zenject;

namespace Assets.Game.Scripts.Battle.Ecs
{
    public class EcsRunner
    {
        private readonly IInstantiator _instantiator;
        private World _world;

        public EcsRunner(IInstantiator instantiator) => _instantiator = instantiator;

        public void Init()
        {
            _world = World.Create();
            _world.UpdateByUnity = true;

            var systemsGroup = _world.CreateSystemsGroup();

            systemsGroup.AddInitializer(_instantiator.Instantiate<InitializeTestEnemy>());

            systemsGroup.AddSystem(_instantiator.Instantiate<ClickInputSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<FieldClickSystem>());
            
            systemsGroup.AddSystem(_instantiator.Instantiate<SpawnPlayerUnitsSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<PlayerUnitFindTargetSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<UnitChaseTargetSystem>());
            
            systemsGroup.AddSystem(_instantiator.Instantiate<AttackSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<HitSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<DamageSystem>());
            
            systemsGroup.AddSystem(_instantiator.Instantiate<MoveSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<SyncPositionSystem>());
            
            
            systemsGroup.AddSystem(_instantiator.Instantiate<InputCleanUpSystem>());
            
            _world.AddSystemsGroup(0, systemsGroup);
        }
    }
}