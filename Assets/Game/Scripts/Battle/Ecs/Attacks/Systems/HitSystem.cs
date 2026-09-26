using Assets.Game.Scripts.Battle.Ecs.AI;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Attacks.Systems
{
    public class HitSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _hitRequests;
        private Filter _hitRequestsForRemoving;
        
        private Stash<FollowTarget> _followTargetStash;
        private Stash<DamageEvent> _damageStash;
        private Stash<Attacker> _attackerStash;
        private Stash<HitRequest> _hitStash;

        public void OnAwake()
        {
            _hitRequests = World.Filter
                .With<HitRequest>()
                .With<FollowTarget>()
                .With<Attacker>()
                .Build();
            
            _hitRequestsForRemoving = World.Filter
                .With<HitRequest>()
                .Build();
            
            _followTargetStash = World.GetStash<FollowTarget>();
            _damageStash = World.GetStash<DamageEvent>();
            _attackerStash = World.GetStash<Attacker>();
            _hitStash = World.GetStash<HitRequest>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var hitRequest in _hitRequests)
            {
                var eventEntity = World.CreateEntity();
                
                ref var target = ref _followTargetStash.Get(hitRequest);
                ref var attacker = ref _attackerStash.Get(hitRequest);

                _damageStash.Set(eventEntity, new()
                    {
                        Target = target.Target,
                        Damage = attacker.Damage,
                    }
                );
                
            }

            foreach (var hitRequest in _hitRequestsForRemoving)
            {
                _hitStash.Remove(hitRequest);
            }
        }
        
        public void Dispose()
        {
        }
    }
}