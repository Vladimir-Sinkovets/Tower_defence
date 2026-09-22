using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Unity.Links
{
    public class TransformLink : MonoBehaviour, IEntityLink
    {
        public void Link(Entity entity, World world) => 
            world.GetStash<TransformComponent>().Set(entity, new TransformComponent() { Reference = transform });

        public void Unlink(Entity entity, World world) => 
            world.GetStash<TransformComponent>().Remove(entity);
    }
}