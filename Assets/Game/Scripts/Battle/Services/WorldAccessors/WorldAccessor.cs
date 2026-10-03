
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Services.WorldAccessors
{
    public class WorldAccessor : IWorldAccessor
    {
        public World World { get; } = World.Create();
    }
}