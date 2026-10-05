using Assets.Game.Scripts.Battle.Ecs.Unity;
using Assets.Game.Scripts.Enemies;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Movement.Systems
{
    public class MoveAnimation : ISystem
    {
        public World World { get; set; }
        
        private Filter _entities;
        
        private Stash<MoveDirection> _moveDirectionStash;
        private Stash<AnimatorComponent> _animatorStash;

        public void OnAwake()
        {
            _entities = World.Filter
                .With<Position>()
                .With<MoveDirection>()
                .With<AnimatorComponent>()
                .Build();

            _moveDirectionStash = World.GetStash<MoveDirection>();
            _animatorStash = World.GetStash<AnimatorComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _entities)
            {
                ref var animator = ref _animatorStash.Get(entity);
                ref var moveDirection = ref _moveDirectionStash.Get(entity);
                
                if (moveDirection.Value.magnitude <= 0.001f)
                    animator.Reference.SetBool(SimpleEnemyAnimationParameters.Walk, false);
                else
                    animator.Reference.SetBool(SimpleEnemyAnimationParameters.Walk, true);
            }
        }
        
        public void Dispose() { }
    }
}