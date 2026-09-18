using Assets.Game.Scripts.Battle;
using Assets.Game.Scripts.Battle.Ecs;
using Assets.Game.Scripts.Battle.Services.Raycasts;
using Assets.Game.Scripts.Battle.Services.UnitFactories;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class BattleInstaller : MonoInstaller
    {
        [SerializeField] private Transform _planeCenter;
        [SerializeField] private UnitsConfig _unitsConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BattleEntryPoint>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlaneRaycastService>().AsSingle();
            
            Container.BindInstance(_planeCenter).AsSingle();

            Container.BindInterfacesAndSelfTo<EcsRunner>().AsSingle();
            
            Container.BindInstance(Camera.main).AsSingle();

            Container.BindInstance(_unitsConfig).AsSingle();
            
            Container.BindInterfacesAndSelfTo<UnitFactory>().AsSingle();
        }
    }
}