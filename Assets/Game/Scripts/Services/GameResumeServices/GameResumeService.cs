using Assets.Game.Scripts.Services.GameStoppers;
using Assets.Game.Scripts.Shared;

namespace Assets.Game.Scripts.Services.GameResumeServices
{
    public class GameResumeService : IGameResumeService
    {
        private readonly IGameStopper _gameStopper;
        
        private Health _castleHealth;

        public GameResumeService(IGameStopper gameStopper) => _gameStopper = gameStopper;

        public void Init(Health castleHealth) => _castleHealth = castleHealth;

        public void Resume()
        {
            _castleHealth.Reset();
            
            _gameStopper.Resume();
        }
    }
}