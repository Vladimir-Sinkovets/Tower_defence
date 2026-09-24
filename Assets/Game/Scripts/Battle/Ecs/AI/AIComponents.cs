using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.AI
{
    public struct EnemyUnit : IComponent { }
    public struct PlayerUnit : IComponent { }
    public struct FollowTarget : IComponent { public Entity Target; }
}