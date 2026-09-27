using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Movement;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Attacks.Systems
{
    public class AttackSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _attacks;
        
        private Stash<AttackRequest> _attackRequestStash;
        private Stash<Attack> _attackStash;
        private Stash<Attacker> _attackerStash;
        private Stash<AttackRange> _attackRangeStash;
        private Stash<Target> _targetStash;
        private Stash<Position> _positionStash;

        public void OnAwake()
        {
            _attacks = World.Filter
                .With<Target>()
                .With<Position>()
                .With<Attack>()
                .With<AttackRange>()
                .With<Attacker>()
                .Build();

            _attackRequestStash = World.GetStash<AttackRequest>();
            _attackStash = World.GetStash<Attack>();
            _attackRangeStash = World.GetStash<AttackRange>();
            _attackerStash = World.GetStash<Attacker>();
            _targetStash = World.GetStash<Target>();
            _positionStash = World.GetStash<Position>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _attacks)
            {
                ref var attack = ref _attackStash.Get(entity);
                ref var attacker = ref _attackerStash.Get(entity);
                ref var attackRange = ref _attackRangeStash.Get(entity);
                ref var target = ref _targetStash.Get(entity);

                if (!_positionStash.Has(target.Value))
                    continue;
                
                ref var targetPosition = ref _positionStash.Get(target.Value);
                ref var position = ref _positionStash.Get(entity);

                if ((targetPosition.Value - position.Value).magnitude < attackRange.Value)
                {
                    if (attack.Timer <= 0)
                    {
                        _attackRequestStash.Set(entity, new AttackRequest());
                        
                        attack.Timer += attacker.TimeBetweenAttacks;
                    }
                }
                
                
                attack.Timer -= deltaTime;
            }
        }

        public void Dispose() { }
    }
}