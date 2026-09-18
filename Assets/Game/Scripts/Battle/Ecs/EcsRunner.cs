using Assets.Game.Scripts.Battle.Ecs.Input.Systems;
using Assets.Game.Scripts.Battle.Ecs.Spawn.Systems;
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

            systemsGroup.AddSystem(_instantiator.Instantiate<ClickInputSystem>());
            systemsGroup.AddSystem(_instantiator.Instantiate<FieldClickSystem>());
            
            systemsGroup.AddSystem(_instantiator.Instantiate<SpawnPlayerUnitsSystem>());
            
            systemsGroup.AddSystem(_instantiator.Instantiate<InputCleanUpSystem>());
            
            _world.AddSystemsGroup(0, systemsGroup);
        }
    }
}