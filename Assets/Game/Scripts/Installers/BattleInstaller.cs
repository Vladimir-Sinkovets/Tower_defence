using Assets.Game.Scripts.Battle;
using Assets.Game.Scripts.Battle.Ecs;
using Assets.Game.Scripts.Battle.Services.EnemySpawnStarters;
using Assets.Game.Scripts.Battle.Services.HudFactories;
using Assets.Game.Scripts.Battle.Services.Raycasts;
using Assets.Game.Scripts.Battle.Services.UnitFactories;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class BattleInstaller : MonoInstaller
    {
        [SerializeField] private Transform _planeCenter;
        [SerializeField] private PlayerUnitsConfig _playerUnitsConfig;
        [SerializeField] private EnemySpawnConfig _enemySpawnConfig;
        [SerializeField] private HudConfig _hudConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BattleEntryPoint>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlaneRaycastService>().AsSingle();
            
            Container.BindInstance(_planeCenter).AsSingle();

            Container.BindInterfacesAndSelfTo<EcsRunner>().AsSingle();
            
            Container.BindInstance(Camera.main).AsSingle();

            Container.BindInstance(_playerUnitsConfig).AsSingle();
            
            Container.BindInterfacesAndSelfTo<UnitFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<HudFactory>().AsSingle();
            
            Container.BindInstance(_hudConfig).AsSingle();
            
            Container.BindInstance(World.Default).AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemySpawnStarter>().AsSingle();
            
            Container.BindInstance(_enemySpawnConfig).AsSingle();
        }
    }
}