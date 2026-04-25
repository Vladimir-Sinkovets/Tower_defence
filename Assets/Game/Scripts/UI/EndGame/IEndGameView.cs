using System;

namespace Assets.Game.Scripts.UI
{
    public interface IEndGameView
    {
        event Action OnRestartButtonClicked;
        event Action OnMenuButtonClicked;
        void Open();
        void ShowWavesCount(int wavesCount);
        void ShowKillsCount(int killsCount);
        void ShowCurrency(int currency);
        void ShowEarnedMetaCurrency(int metaCurrency);
    }
}