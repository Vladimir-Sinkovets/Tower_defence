using Assets.Game.Scripts.Configs;

namespace Assets.Game.Scripts.Services.ContinueGameServices
{
    public class ContinueGameService : IContinueGameService
    {
        private readonly GameplayConfig _config;

        private int _continuesRemain;

        public ContinueGameService(GameplayConfig config) => _continuesRemain = config.ContinuesAfterDeath;
        
        public bool HasContinues() => _continuesRemain > 0;

        public void UseContinue() => _continuesRemain--;
    }
}