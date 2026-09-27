using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.AI
{
    public struct Team : IComponent
    {
        public int Index;
    }
    public struct Target : IComponent { public Entity Value; }
}