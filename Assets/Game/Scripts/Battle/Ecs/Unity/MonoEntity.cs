using Assets.Game.Scripts.Battle.Ecs.Unity.Links;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Unity
{
    public class MonoEntity : MonoBehaviour
    {
        private IEntityLink[] _links;
        
        public Entity Entity { get; private set; }
        public World World { get; private set; }

        public void Bind(Entity entity, World world)
        {
            Entity = entity;
            World = world;

            _links = GetComponentsInChildren<IEntityLink>();

            World.GetStash<View>().Set(entity, new View() { MonoEntity = this });

            foreach (var link in _links)
            {
                link.Link(entity, world);
            }
        }

        public void Unbind()
        {
            World.GetStash<View>().Remove(Entity);
            
            foreach (var link in _links)
            {
                link.Unlink(Entity, World);
            }
            
            Entity = default;
            World = null;
        }
    }
}