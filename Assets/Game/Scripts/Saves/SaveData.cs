using System;
using Assets.Game.Scripts.Common.Types;

namespace Assets.Game.Scripts.Saves
{
    public class SaveData : IDisposable
    {
        public event Action OnUpgradesChanged;
        public event Action MetaCurrencyChanged;
        
        private ObservableDictionary<string, int> _upgrades = new ObservableDictionary<string, int>();
        private int _metaCurrency;

        public SaveData() => _upgrades.OnChanged += OnUpgradesChangedHandler;

        
        public int MetaCurrency
        {
            get => _metaCurrency;
            set
            {
                _metaCurrency = value;
                MetaCurrencyChanged?.Invoke();
            }
        }

        public int WavesRecord { get; set; }

        public ObservableDictionary<string, int> Upgrades
        {
            get => _upgrades;
            set
            {
                _upgrades.OnChanged -= OnUpgradesChangedHandler;
                _upgrades = value;
                _upgrades.OnChanged += OnUpgradesChangedHandler;
                
                OnUpgradesChanged?.Invoke();
            }
        }

        
        private void OnUpgradesChangedHandler() => OnUpgradesChanged?.Invoke();

        public void Dispose() => _upgrades.OnChanged -= OnUpgradesChangedHandler;
    }
}