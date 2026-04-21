using System;

namespace Assets.Game.Scripts.UI
{
    public interface IEndGameView
    {
        event Action OnRestartButtonClicked;
        event Action OnMenuButtonClicked;
        void Open(int wavesCount, int killedEnemyCount, int currencyCount, int metaCurrencyCount);
    }
}