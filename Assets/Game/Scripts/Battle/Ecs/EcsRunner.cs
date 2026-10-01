using Assets.Game.Scripts.Battle.Ecs.AI.Systems;
using Assets.Game.Scripts.Battle.Ecs.Attacks.Systems;
using Assets.Game.Scripts.Battle.Ecs.CurrencyBank.Systems;
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

            AddInitializeSystems(systemsGroup);
            
            AddInputSystems(systemsGroup);
            AddSpawnSystems(systemsGroup);
            AddAISystems(systemsGroup);
            AddAttackSystems(systemsGroup);
            AddDeathSystems(systemsGroup);
            AddCurrencySystems(systemsGroup);
            AddMoveSystems(systemsGroup);
            AddCleanSystem(systemsGroup);

            _world.AddSystemsGroup(0, systemsGroup);
        }

        private void AddInitializeSystems(SystemsGroup systemsGroup)
        {
            systemsGroup.AddInitializer(_instantiator.Instantiate<SetupStartCurrencySystem>());
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
            systemsGroup.AddSystem(_instantiator.Instantiate<TargetingSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<FollowTargetSystem>());
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
        }

        private void AddCurrencySystems(SystemsGroup systemsGroup)
        {
            systemsGroup.AddSystem(_instantiator.Instantiate<CurrencySystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<CurrencyUISystem>());
        }

        private void AddMoveSystems(SystemsGroup systemsGroup)
        {
            systemsGroup.AddSystem(_instantiator.Instantiate<MoveSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<RotateSystem>());
            
            systemsGroup.AddSystem(_instantiator.Instantiate<MoveAnimation>());
            
            systemsGroup.AddSystem(_instantiator.Instantiate<SyncPositionSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<SyncRotationSystem>());
        }

        private void AddCleanSystem(SystemsGroup systemsGroup)
        {
            systemsGroup.AddSystem(_instantiator.Instantiate<InputCleanUpSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<DestroyViewSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<RemoveDeadSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<RemoveDeathEventsSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<CurrencyEventCleanSystem>());
        }
    }
}