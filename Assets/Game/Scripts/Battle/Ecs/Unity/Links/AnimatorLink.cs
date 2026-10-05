using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Unity.Links
{
    public class AnimatorLink : MonoBehaviour, IEntityLink
    {
        public void Link(Entity entity, World world) => 
            world.GetStash<AnimatorComponent>().Set(entity, new() { Reference = GetComponentInChildren<Animator>() });

        public void Unlink(Entity entity, World world) => 
            world.GetStash<AnimatorComponent>().Remove(entity);
    }
}