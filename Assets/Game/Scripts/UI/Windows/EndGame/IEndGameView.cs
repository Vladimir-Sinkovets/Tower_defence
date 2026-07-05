using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.UI.Windows.EndGame
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
        UniTask Close(CancellationToken token = default);
    }
}