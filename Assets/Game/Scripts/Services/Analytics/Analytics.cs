using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.CurrencyBanks;

namespace Assets.Game.Scripts.Services.Analytics
{
    public class Analytics : IAnalytics
    {
        private const string GameStartedEventName = "game_started";
        private const string MetaCurrencyTotalParameterName = "meta_currency_total";
        
        private const string WaveStartedEventName = "wave_started";
        private const string EnemiesToSpawnParameterName = "enemies_to_spawn";
        
        private const string WaveCompletedEventName = "wave_completed";
        private const string TowersBuiltParameterName = "towers_built";
        
        private const string TowerBuiltEventName = "tower_built";
        private const string CoinsSpentParameterName = "coins_spent";
        private const string TowersTotalParameterName = "towers_total";
        
        private const string BuildRejectedEventName = "build_rejected";
        private const string CastleDamagedEventName = "castle_damaged";
        private const string GameOverEventName = "game_over";
        private const string SessionRestartedEventName = "session_restarted";
        private const string ReturnedToMenuEventName = "returned_to_menu";
        
        private const string WaveNumberParameterName = "wave_number";
        private const string CoinsRemainingParameterName = "coins_remaining";
        
        private readonly SaveData _saveData;
        private readonly CurrencyBank _currencyBank;
        private readonly IAnalyticsProvider _analyticsProvider;

        public Analytics(SaveData saveData, CurrencyBank currencyBank, IAnalyticsProvider analyticsProvider)
        {
            _saveData = saveData;
            _currencyBank = currencyBank;
            _analyticsProvider = analyticsProvider;
        }

        public void GameStarted()
        {
            var metaCurrency = _saveData.MetaCurrency;
            
            _analyticsProvider.LogEvent(GameStartedEventName, new AnalyticsParameter(MetaCurrencyTotalParameterName, metaCurrency));
        }

        public void WaveStarted(int waveNumber, int enemiesToSpawn) =>
            _analyticsProvider.LogEvent(WaveStartedEventName,
                new AnalyticsParameter(WaveNumberParameterName, waveNumber),
                new AnalyticsParameter(EnemiesToSpawnParameterName, enemiesToSpawn));

        public void WaveCompleted(int waveNumber, int towersBuilt)
        {
            var coinsRemaining = _currencyBank.Total;
            
            _analyticsProvider.LogEvent(WaveCompletedEventName,
                new AnalyticsParameter(WaveNumberParameterName, waveNumber),
                new AnalyticsParameter(TowersBuiltParameterName, towersBuilt),
                new AnalyticsParameter(CoinsRemainingParameterName, coinsRemaining));
        }

        public void TowerBuilt(int coinsSpent, int towersTotal, int waveNumber)
        {
            var coinsRemaining = _currencyBank.Total;
            
            _analyticsProvider.LogEvent(TowerBuiltEventName,
                new AnalyticsParameter(WaveNumberParameterName, waveNumber),
                new AnalyticsParameter(CoinsSpentParameterName, coinsSpent),
                new AnalyticsParameter(CoinsRemainingParameterName, coinsRemaining),
                new AnalyticsParameter(TowersTotalParameterName, towersTotal));
        }

        public void BuildRejected() => _analyticsProvider.LogEvent(BuildRejectedEventName);

        public void CastleDamaged() => _analyticsProvider.LogEvent(CastleDamagedEventName);

        public void GameOver() => _analyticsProvider.LogEvent(GameOverEventName);

        public void SessionRestarted() => _analyticsProvider.LogEvent(SessionRestartedEventName);

        public void ReturnedToMenu() => _analyticsProvider.LogEvent(ReturnedToMenuEventName);
    }
}