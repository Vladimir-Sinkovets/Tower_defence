using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Battle.UI.Windows.EndGamePanel
{
    public interface IEndGameView
    {
        event Action OnRestartButtonClicked;
        event Action OnMenuButtonClicked;
        void Open();
        UniTask Close(CancellationToken token = default);
        void ShowWavesCount(int wavesCount);
        void ShowKillsCount(int killsCount);
        void ShowEarnedMetaCurrency(int metaCurrency);
    }
}