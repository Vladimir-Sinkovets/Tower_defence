using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Arena.UI.Windows.EndGame
{
    public interface IEndGameView
    {
        event Action OnMenuButtonClicked;
        void Open();
        void ShowKillsCount(int killsCount);
        void ShowEarnedMetaCurrency(int metaCurrency);
        UniTask Close(CancellationToken token = default);
    }
}