using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.AI.Systems
{
    public class AttackSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _attackers;
        
        private Stash<Attack> _attackStash;

        public void OnAwake()
        {
            _attackers = World.Filter
                .With<Attack>()
                .Build();

            _attackStash = World.GetStash<Attack>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var attacker in _attackers)
            {
                
            }
        }

        public void Dispose() { }
    }
}