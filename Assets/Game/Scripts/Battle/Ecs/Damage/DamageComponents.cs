using System;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Damage
{
    public struct AttackRequest : IComponent {  }
    [Serializable]
    public struct Attacker : IComponent
    {
        public int Damage;
    }
    public struct HitRequest : IComponent { }
    [Serializable]
    public struct DamageEvent : IComponent
    {
        public Entity Target;
        public int Damage;
    }
}