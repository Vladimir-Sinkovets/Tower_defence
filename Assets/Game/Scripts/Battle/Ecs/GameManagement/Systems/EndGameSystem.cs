using Assets.Game.Scripts.Battle.Common;
using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Extensions;
using Assets.Game.Scripts.Battle.UI.Windows.EndGamePanel;
using Scellecs.Morpeh;
using UnityEditor.Localization.Plugins.XLIFF.V12;

namespace Assets.Game.Scripts.Battle.Ecs.GameManagement.Systems
{
    public class EndGameSystem : ISystem
    {
        public World World { get; set; }
        
        private readonly IWindowsManager _windowsManager;
        
        private Filter _units;
        
        private Stash<Team> _teamStash;
        
        private Entity _gameManagerEntity;
        private Stash<GameManager> _gameManagerStash;

        public EndGameSystem(IWindowsManager windowsManager) => _windowsManager = windowsManager;

        public void OnAwake()
        {
            _units = World.Filter
                .With<Team>()
                .Build();

            _gameManagerEntity = World.GetManagerEntity();

            _teamStash = World.GetStash<Team>();
            _gameManagerStash = World.GetStash<GameManager>();
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_gameManagerStash.Get(_gameManagerEntity).GameStarted)
                return;
            
            var count = 0;

            foreach (var entity in _units)
            {
                ref var team = ref _teamStash.Get(entity);

                if (team.Index == TeamIndexes.Player)
                    count++;
            }

            if (count <= 0)
            {
                ref var manager = ref _gameManagerStash.Get(_gameManagerEntity);

                manager.GameEnded = true;
                
                _windowsManager.Open(WindowType.EndGame);
            }
        }
        
        public void Dispose() { }
    }
}