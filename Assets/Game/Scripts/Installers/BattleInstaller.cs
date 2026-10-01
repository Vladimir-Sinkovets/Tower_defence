using Assets.Game.Scripts.Battle;
using Assets.Game.Scripts.Battle.Configs;
using Assets.Game.Scripts.Battle.Ecs;
using Assets.Game.Scripts.Battle.Ecs.CurrencyBank.Views;
using Assets.Game.Scripts.Battle.Services.BattleResultCalculators;
using Assets.Game.Scripts.Battle.Services.EnemySpawnStarters;
using Assets.Game.Scripts.Battle.Services.HudFactories;
using Assets.Game.Scripts.Battle.Services.Raycasts;
using Assets.Game.Scripts.Battle.Services.UnitFactories;
using Assets.Game.Scripts.Battle.UI.Windows.EndGamePanel;
using Scellecs.Morpeh;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Installers
{
    public class BattleInstaller : MonoInstaller
    {
        [SerializeField] private Transform _planeCenter;
        [SerializeField] private EnemySpawnConfig _enemySpawnConfig;
        [SerializeField] private HudConfig _hudConfig;
        [SerializeField] private CurrencyView _currencyView;
        [SerializeField] private UnitsConfig _unitsConfig;
        [SerializeField] private BattleConfig _battleConfig;
        [SerializeField] private WindowViewsConfig _windowViewsConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BattleEntryPoint>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlaneRaycastService>().AsSingle();
            
            Container.BindInstance(_planeCenter).AsSingle();

            Container.BindInterfacesAndSelfTo<EcsRunner>().AsSingle();
            
            Container.BindInstance(Camera.main).AsSingle();

            Container.BindInterfacesAndSelfTo<UnitFactory>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<HudFactory>().AsSingle();
            
            Container.BindInstance(_hudConfig).AsSingle();
            
            Container.BindInstance(World.Default).AsSingle();
            
            Container.BindInterfacesAndSelfTo<EnemySpawnStarter>().AsSingle();
            
            Container.BindInstance(_enemySpawnConfig).AsSingle();

            Container.Bind<ICurrencyView>().FromInstance(_currencyView).AsSingle();
            
            Container.BindInstance(_unitsConfig).AsSingle();
            
            Container.BindInstance(_battleConfig).AsSingle();
            
            Container.BindInterfacesTo<WindowsManager>().AsSingle();
            
            Container.BindInterfacesTo<WindowFactory>().AsSingle();
            
            Container.BindInstance(_windowViewsConfig).AsSingle();
            
            Container.BindInterfacesTo<BattleResultCalculator>().AsSingle();
        }
    }
}