using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Attacks
{
    public struct AttackRequest : IComponent {  }
    public struct Attacker : IComponent
    {
        public int Damage;
        public float TimeBetweenAttacks;
    }
    public struct HitRequest : IComponent { }
    public struct DamageEvent : IComponent
    {
        public Entity Target;
        public int Damage;
    }

    public struct Attack : IComponent
    {
        public float Timer;
        public float NextTimeAttack;
    }
}