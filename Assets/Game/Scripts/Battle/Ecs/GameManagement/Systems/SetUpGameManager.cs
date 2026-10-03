using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.GameManagement.Systems
{
    public class SetUpGameManager : IInitializer
    {
        public World World { get; set; }

        public void OnAwake() => 
            World.GetStash<GameManager>().Set(World.CreateEntity());

        public void Dispose() { }
    }
}