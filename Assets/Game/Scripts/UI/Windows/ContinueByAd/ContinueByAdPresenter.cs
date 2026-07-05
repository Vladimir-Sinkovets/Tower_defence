using Assets.Game.Scripts.Services.Ads;
using Assets.Game.Scripts.Services.GameOverManagers;
using Assets.Game.Scripts.Services.GameResumeServices;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.UI.Windows.ContinueByAd
{
    public class ContinueByAdPresenter : IWindowPresenter
    {
        private readonly IContinueByAdView _view;
        private readonly IGameResumeService _gameResumeService;
        private readonly IAdsRewardService _adsRewardService;
        private readonly IWindowsManager _windowsManager;
        private readonly GameOverManager _gameOverManager;

        public ContinueByAdPresenter(
            IContinueByAdView view,
            IGameResumeService gameResumeService,
            IAdsRewardService adsRewardService,
            IWindowsManager windowsManager,
            GameOverManager gameOverManager)
        {
            _view = view;
            _gameResumeService = gameResumeService;
            _adsRewardService = adsRewardService;
            _windowsManager = windowsManager;
            _gameOverManager = gameOverManager;
        }
        
        public void Activate()
        {
            _view.OnDeclineButtonClicked += OnDeclineButtonClickedHandler;
            _view.OnContinueButtonClicked += OnContinueButtonClickedHandler;
            
            _view.Open();
        }

        public void Deactivate()
        {
            _view.OnDeclineButtonClicked -= OnDeclineButtonClickedHandler;
            _view.OnContinueButtonClicked -= OnContinueButtonClickedHandler;
            
            _view.Close();
        }

        private void OnContinueButtonClickedHandler() => ShowAdAsync().Forget();

        private async UniTask ShowAdAsync()
        {
            await _adsRewardService.LoadAdAsync();

            await _adsRewardService.ShowAdAsync();

            _gameResumeService.Resume();

            _windowsManager.Close(WindowType.ContinueByAd);
        }
        
        private void OnDeclineButtonClickedHandler()
        {
            _windowsManager.Close(WindowType.ContinueByAd);
            
            _gameOverManager.GameOver();
        }
    }
}