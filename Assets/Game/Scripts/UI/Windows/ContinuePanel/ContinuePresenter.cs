using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Ads;
using Assets.Game.Scripts.Services.ContinueGameServices;
using Assets.Game.Scripts.Services.GameOverManagers;
using Assets.Game.Scripts.Services.GameResumeServices;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.UI.Windows.ContinuePanel
{
    public class ContinuePresenter : IWindowPresenter
    {
        private readonly IContinueView _view;
        private readonly IGameResumeService _gameResumeService;
        private readonly IAdsRewardService _adsRewardService;
        private readonly IWindowsManager _windowsManager;
        private readonly GameOverManager _gameOverManager;
        private readonly IContinueGameService _continueGameService;
        private readonly ISaveService _saveService;

        public ContinuePresenter(
            IContinueView view,
            IGameResumeService gameResumeService,
            IAdsRewardService adsRewardService,
            IWindowsManager windowsManager,
            GameOverManager gameOverManager,
            IContinueGameService continueGameService,
            ISaveService saveService)
        {
            _view = view;
            _gameResumeService = gameResumeService;
            _adsRewardService = adsRewardService;
            _windowsManager = windowsManager;
            _gameOverManager = gameOverManager;
            _continueGameService = continueGameService;
            _saveService = saveService;
        }
        
        public void Activate()
        {
            _view.OnDeclineButtonClicked += OnDeclineButtonClickedHandler;
            _view.OnContinueButtonClicked += OnContinueButtonClickedHandler;
            
            _view.Open();
            
            _view.SetContinueButtonActive(_continueGameService.HasContinues());
        }

        public void Deactivate()
        {
            _view.OnDeclineButtonClicked -= OnDeclineButtonClickedHandler;
            _view.OnContinueButtonClicked -= OnContinueButtonClickedHandler;
            
            _view.Close();
        }

        private void OnContinueButtonClickedHandler()
        {
            ShowAdAsync().Forget();
            
            _continueGameService.UseContinue();
        }

        private async UniTask ShowAdAsync()
        {
            if (!_saveService.IsaAdsDisabled)
                await _adsRewardService.ShowAdAsync();

            _gameResumeService.Resume();

            _windowsManager.Close(WindowType.ContinueByAd);
        }
        
        private void OnDeclineButtonClickedHandler()
        {
            _windowsManager.Close(WindowType.ContinueByAd);
            
            _gameOverManager.GameOverAsync();
        }
    }
}