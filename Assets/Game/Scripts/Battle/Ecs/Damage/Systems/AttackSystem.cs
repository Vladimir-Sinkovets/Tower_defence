using Assets.Game.Scripts.Battle.Ecs.Unity;
using Assets.Game.Scripts.Enemies;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Damage.Systems
{
    public class AttackSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _attackers;
        private Filter _attackersForRemoving;
        
        private Stash<AnimatorComponent> _animatorStash;
        private Stash<AttackRequest> _attackRequestStash;

        public void OnAwake()
        {
            _attackers = World.Filter
                .With<AttackRequest>()
                .With<AnimatorComponent>()
                .Build();

            _attackersForRemoving = World.Filter
                .With<AttackRequest>()
                .Build();
            
            _animatorStash = World.GetStash<AnimatorComponent>();
            _attackRequestStash = World.GetStash<AttackRequest>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var attacker in _attackers)
            {
                ref var animator = ref _animatorStash.Get(attacker);

                animator.Reference.SetTrigger(SimpleEnemyAnimationParameters.Attack);
            }

            foreach (var attacker in _attackersForRemoving)
            {
                _attackRequestStash.Remove(attacker);
            }
        }

        public void Dispose() { }
    }
}