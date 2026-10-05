using Assets.Game.Scripts.Battle.Ecs.Attacks;
using Assets.Game.Scripts.Battle.Ecs.Unity.Links;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Unity.AnimationEventHandlers
{
    public class EnemyAnimationEventHandler : MonoBehaviour, IEntityLink
    {
        [SerializeField] private Assets.Game.Scripts.Enemies.EnemyAnimationEventHandler _animationEventHandler;
     
        private Entity _entity;
        private World _world;

        
        public void Link(Entity entity, World world)
        {
            _entity = entity;
            _world = world;
            
            _animationEventHandler.OnHit += OnHitHandler;
        }

        public void Unlink(Entity entity, World world)
        {
            _entity = default;
            _world = null;
            
            _animationEventHandler.OnHit -= OnHitHandler;
        }

        private void OnHitHandler() => _world?.GetStash<HitRequest>().Set(_entity, new());
    }
}