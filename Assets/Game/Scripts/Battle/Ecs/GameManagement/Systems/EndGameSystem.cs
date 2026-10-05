using Assets.Game.Scripts.Battle.Common;
using Assets.Game.Scripts.Battle.Ecs.AI;
using Assets.Game.Scripts.Battle.Ecs.Extensions;
using Assets.Game.Scripts.Battle.Services.EndGame;
using Assets.Game.Scripts.Battle.UI.Windows.EndGamePanel;
using Scellecs.Morpeh;

namespace Assets.Game.Scripts.Battle.Ecs.GameManagement.Systems
{
    public class EndGameSystem : ISystem
    {
        public World World { get; set; }
        
        private Filter _units;
        
        private Stash<Team> _teamStash;
        private Stash<GameManager> _gameManagerStash;
        
        private Entity _gameManagerEntity;

        private readonly IEndGameService _endGameService;
        
        public EndGameSystem(IEndGameService endGameService) => _endGameService = endGameService;

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

                if (manager.GameEnded)
                    return;
                
                manager.GameEnded = true;

                _endGameService.EndGame();                
            }
        }
        
        public void Dispose() { }
    }
}