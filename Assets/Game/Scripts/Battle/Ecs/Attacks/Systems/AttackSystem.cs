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

        public void OnAwake()
        {
            _attacks = World.Filter
                .With<Attack>()
                .With<Attacker>()
                .Build();

            _attackRequestStash = World.GetStash<AttackRequest>();
            _attackStash = World.GetStash<Attack>();
            _attackerStash = World.GetStash<Attacker>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _attacks)
            {
                ref var attack = ref _attackStash.Get(entity);
                ref var attacker = ref _attackerStash.Get(entity);

                if (attack.Timer >= attack.NextTimeAttack)
                {
                    _attackRequestStash.Set(entity, new AttackRequest());
                    
                    attack.NextTimeAttack += attacker.TimeBetweenAttacks;
                }
                
                attack.Timer += deltaTime;
            }
        }

        public void Dispose() { }
    }
}