using Assets.Game.Scripts.Services.GameplayOrchestrators;
using Zenject;

namespace Assets.Game.Scripts
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly IGameplayOrchestrator _gameplayOrchestrator;

        public GameplayEntryPoint(IGameplayOrchestrator gameplayOrchestrator) => _gameplayOrchestrator = gameplayOrchestrator;

        public void Initialize() => _gameplayOrchestrator.Init();
    }
}