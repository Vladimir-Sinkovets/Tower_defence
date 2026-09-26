using Assets.Game.Scripts.Battle.Ecs.AI.Systems;
using Assets.Game.Scripts.Battle.Ecs.Attacks.Systems;
using Assets.Game.Scripts.Battle.Ecs.HealthFeature.Systems;
using Assets.Game.Scripts.Battle.Ecs.Input.Systems;
using Assets.Game.Scripts.Battle.Ecs.Movement.Systems;
using Assets.Game.Scripts.Battle.Ecs.Spawn.Systems;
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
            _world = World.Default ?? World.Create();

            _world.UpdateByUnity = true;

            var systemsGroup = _world.CreateSystemsGroup();

            AddInputSystems(systemsGroup);

            AddSpawnSystems(systemsGroup);

            AddAISystems(systemsGroup);

            AddAttackSystems(systemsGroup);

            AddDeathSystems(systemsGroup);

            AddMoveSystems(systemsGroup);

            systemsGroup.AddSystem(_instantiator.Instantiate<InputCleanUpSystem>());
            
            _world.AddSystemsGroup(0, systemsGroup);
        }

        private void AddInputSystems(SystemsGroup systemsGroup)
        {
            systemsGroup.AddSystem(_instantiator.Instantiate<ClickInputSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<FieldClickSystem>());
        }

        private void AddSpawnSystems(SystemsGroup systemsGroup)
        {
            systemsGroup.AddSystem(_instantiator.Instantiate<SpawnPlayerUnitsSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<SpawnEnemyUnitsSystem>());
        }

        private void AddAISystems(SystemsGroup systemsGroup)
        {
            systemsGroup.AddSystem(_instantiator.Instantiate<PlayerUnitFindTargetSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<UnitChaseTargetSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<ClearTargetSystem>());
        }

        private void AddAttackSystems(SystemsGroup systemsGroup)
        {
            systemsGroup.AddSystem(_instantiator.Instantiate<AttackSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<AttackRequestHandlerSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<HitSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<DamageSystem>());
        }

        private void AddDeathSystems(SystemsGroup systemsGroup)
        {
            systemsGroup.AddSystem(_instantiator.Instantiate<DeathSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<DestroyViewSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<RemoveDeadSystem>());
        }

        private void AddMoveSystems(SystemsGroup systemsGroup)
        {
            systemsGroup.AddSystem(_instantiator.Instantiate<MoveSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<RotateSystem>());
            
            systemsGroup.AddSystem(_instantiator.Instantiate<MoveAnimation>());
            
            systemsGroup.AddSystem(_instantiator.Instantiate<SyncPositionSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<SyncRotationSystem>());
        }
    }
}