using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.CurrencyBank
{
    public struct Currency : IComponent { public int Value; }
    public struct CurrencyChangedEvent : IComponent { public int Value; }
}