using Assets.Game.Scripts.Battle;
using Assets.Game.Scripts.Battle.Ecs;
using Assets.Game.Scripts.Battle.Services.Raycasts;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class BattleInstaller : MonoInstaller
    {
        [SerializeField] private Transform _planeCenter;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BattleEntryPoint>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlaneRaycastService>().AsSingle();
            
            Container.BindInstance(_planeCenter).AsSingle();

            Container.BindInterfacesAndSelfTo<EcsRunner>().AsSingle();
            
            Container.BindInstance(Camera.main).AsSingle();
        }
    }
}