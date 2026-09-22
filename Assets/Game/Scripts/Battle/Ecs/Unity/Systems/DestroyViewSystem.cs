using Assets.Game.Scripts.Battle.Ecs.HealthFeature;
using Scellecs.Morpeh;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.Ecs.Unity.Systems
{
    public class DestroyViewSystem : ISystem
    {
        private Filter _dead;
        private Stash<View> _viewStash;

        public World World { get; set; }

        public void OnAwake()
        {
            _dead = World.Filter
                .With<Dead>()
                .With<View>()
                .Build();

            _viewStash = World.GetStash<View>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _dead)
            {
                ref var view = ref _viewStash.Get(entity);

                var viewObject = view.MonoEntity.gameObject;
                
                view.MonoEntity.Unbind();
                
                Object.Destroy(viewObject);
            }
        }
        
        public void Dispose() { }
    }
}