using System;
using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.Ads;
using Assets.Game.Scripts.Services.ContinueGameServices;
using Assets.Game.Scripts.Services.GameOverManagers;
using Assets.Game.Scripts.Services.GameResumeServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Windows.ContinuePanel
{
    public class ContinuePresenter : IWindowPresenter
    {
        private readonly IContinueView _view;
        private readonly IGameResumeService _gameResumeService;
        private readonly IAdsRewardService _adsRewardService;
        private readonly IWindowsManager _windowsManager;
        private readonly IGameOverManager _gameOverManager;
        private readonly IContinueGameService _continueGameService;
        private readonly ISaveService _saveService;

        public ContinuePresenter(
            IContinueView view,
            IGameResumeService gameResumeService,
            IAdsRewardService adsRewardService,
            IWindowsManager windowsManager,
            IGameOverManager gameOverManager,
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
            _view.SetContinueButtonActive(false);

            ShowAdAsync().Forget();
        }

        private async UniTask ShowAdAsync()
        {
            if (!_saveService.IsAdsDisabled)
            {
                try
                {
                    await _adsRewardService.ShowAdAsync();
                }
                catch (Exception e)
                {
                    Debug.LogError($"Show ads to continue error, {e.Message}");
                    
                    if (_continueGameService.HasContinues())
                        _view.SetContinueButtonActive(true);
                    
                    return;
                }
            }
            
            _continueGameService.UseContinue();
            
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