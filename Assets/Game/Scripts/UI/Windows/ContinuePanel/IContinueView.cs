using System;

namespace Assets.Game.Scripts.UI.Windows.ContinuePanel
{
    public interface IContinueView
    {
        event Action OnContinueButtonClicked; 
        event Action OnDeclineButtonClicked; 
        void Open();
        void Close();
        void SetContinueButtonActive(bool isActive);
    }
}