using System;

namespace Assets.Game.Scripts.UI.Windows.ContinueByAd
{
    public interface IContinueByAdView
    {
        event Action OnContinueButtonClicked; 
        event Action OnDeclineButtonClicked; 
        void Open();
        void Close();
    }
}