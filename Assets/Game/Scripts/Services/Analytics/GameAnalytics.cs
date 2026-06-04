using Assets.Game.Scripts.Saves;
using Assets.Game.Scripts.Services.CurrencyBanks;
using Firebase.Analytics;

namespace Assets.Game.Scripts.Services.Analytics
{
    public class GameAnalytics : IGameAnalytics
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
        
        private readonly ISaveService _saveService;
        private readonly CurrencyBank _currencyBank;

        public GameAnalytics(ISaveService saveService, CurrencyBank currencyBank)
        {
            _saveService = saveService;
            _currencyBank = currencyBank;
        }

        public void GameStarted()
        {
            var metaCurrency = _saveService.GetSaveData().MetaCurrency;
            
            FirebaseAnalytics.LogEvent(GameStartedEventName, new Parameter(MetaCurrencyTotalParameterName, metaCurrency));
        }

        public void WaveStarted(int waveNumber, int enemiesToSpawn) =>
            FirebaseAnalytics.LogEvent(WaveStartedEventName,
                new Parameter(WaveNumberParameterName, waveNumber),
                new Parameter(EnemiesToSpawnParameterName, enemiesToSpawn));

        public void WaveCompleted(int waveNumber, int towersBuilt)
        {
            var coinsRemaining = _currencyBank.Total;
            
            FirebaseAnalytics.LogEvent(WaveCompletedEventName,
                new Parameter(WaveNumberParameterName, waveNumber),
                new Parameter(TowersBuiltParameterName, towersBuilt),
                new Parameter(CoinsRemainingParameterName, coinsRemaining));
        }

        public void TowerBuilt(int coinsSpent, int towersTotal, int waveNumber)
        {
            var coinsRemaining = _currencyBank.Total;
            
            FirebaseAnalytics.LogEvent(TowerBuiltEventName,
                new Parameter(WaveNumberParameterName, waveNumber),
                new Parameter(CoinsSpentParameterName, coinsSpent),
                new Parameter(CoinsRemainingParameterName, coinsRemaining),
                new Parameter(TowersTotalParameterName, towersTotal));
        }

        public void BuildRejected() => FirebaseAnalytics.LogEvent(BuildRejectedEventName);

        public void CastleDamaged() => FirebaseAnalytics.LogEvent(CastleDamagedEventName);

        public void GameOver() => FirebaseAnalytics.LogEvent(GameOverEventName);

        public void SessionRestarted() => FirebaseAnalytics.LogEvent(SessionRestartedEventName);

        public void ReturnedToMenu() => FirebaseAnalytics.LogEvent(ReturnedToMenuEventName);
    }
}