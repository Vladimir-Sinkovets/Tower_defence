using Assets.Game.Scripts.Battle.Ecs.Damage;
using Assets.Game.Scripts.Battle.Ecs.Unity.Links;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Unity.AnimationEventHandlers
{
    public class EnemyAnimationEventHandler : MonoBehaviour, IEntityLink
    {
        private Entity _entity;
        private World _world;

        public void Link(Entity entity, World world)
        {
            _entity = entity;
            _world = world;
        }

        public void Unlink(Entity entity, World world)
        {
            _entity = default;
            _world = null;
        }

        public void AttackAnimationHitEventHandler() => 
            _world.GetStash<HitRequest>().Set(_entity, new());
    }
}