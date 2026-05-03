using Assets.Game.Scripts.Services;
using Zenject;

namespace Assets.Game.Scripts
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly GameplayOrchestrator _gameplayOrchestrator;

        public GameplayEntryPoint(GameplayOrchestrator gameplayOrchestrator) => _gameplayOrchestrator = gameplayOrchestrator;

        public void Initialize() => _gameplayOrchestrator.Init();
    }
}