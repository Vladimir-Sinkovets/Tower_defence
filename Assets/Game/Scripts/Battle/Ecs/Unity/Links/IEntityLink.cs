using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Unity.Links
{
    public interface IEntityLink
    {
        void Link(Entity entity, World world);
        void Unlink(Entity entity, World world);
    }
}