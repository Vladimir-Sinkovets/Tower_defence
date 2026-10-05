using Assets.Game.Scripts.Battle.Services.BattleResultCalculators;
using Assets.Game.Scripts.Battle.UI.Windows.EndGamePanel;
using Assets.Game.Scripts.Saves;

namespace Assets.Game.Scripts.Battle.Services.EndGame
{
    public class EndGameService : IEndGameService
    {
        private readonly IWindowsManager _windowsManager;
        private readonly IBattleResultCalculator _battleResultCalculator;
        private readonly GameDataHolder _gameDataHolder;
        private readonly ISaveService _saveService;

        public EndGameService(
            IWindowsManager windowsManager,
            IBattleResultCalculator battleResultCalculator,
            GameDataHolder gameDataHolder,
            ISaveService saveService)
        {
            _windowsManager = windowsManager;
            _battleResultCalculator = battleResultCalculator;
            _gameDataHolder = gameDataHolder;
            _saveService = saveService;
        }

        public void EndGame()
        {
            var result = _battleResultCalculator.GetGameOverResult();
            
            _gameDataHolder.Data.MetaCurrency += result.EarnedMetaCurrency;
            
            _windowsManager.Open(WindowType.EndGame);
        }
    }

    public interface IEndGameService
    {
        void EndGame();
    }
}