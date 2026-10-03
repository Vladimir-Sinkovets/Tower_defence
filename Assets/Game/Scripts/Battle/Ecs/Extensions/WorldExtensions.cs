using Assets.Game.Scripts.Battle.Ecs.GameManagement;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.Extensions
{
    public static class WorldExtensions
    {
        public static Entity GetManagerEntity(this World world) =>
            world.Filter.With<GameManager>().Build().First();
    }
}