using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Saves
{
    [Serializable]
    public class SaveData
    {
        public event Action OnChanged;
        
        private Dictionary<string, int> _upgrades = new();
        private int _metaCurrency;
        private int _wavesRecord;
        private bool _isAdsDisabled;
        private DateTime _lastSaveDate;

        public Dictionary<string, int> Upgrades
        {
            get => _upgrades;
            set
            {
                _upgrades = value;
                OnChanged?.Invoke();
            }
        }

        public int MetaCurrency
        {
            get => _metaCurrency;
            set
            {
                _metaCurrency = value;
                OnChanged?.Invoke();
            }
        }

        public int WavesRecord
        {
            get => _wavesRecord;
            set
            {
                _wavesRecord = value;
                OnChanged?.Invoke();
            }
        }

        public bool IsAdsDisabled
        {
            get => _isAdsDisabled;
            set
            {
                _isAdsDisabled = value;
                OnChanged?.Invoke();
            }
        }

        public DateTime LastSaveDate
        {
            get => _lastSaveDate;
            set
            {
                _lastSaveDate = value;
                OnChanged?.Invoke();
            }
        }

        public static SaveData Default => new SaveData
        {
            MetaCurrency = 0,
            WavesRecord = 0,
            IsAdsDisabled = false,
            Upgrades = new Dictionary<string, int>(),
            LastSaveDate = DateTime.MinValue,
        };
    }
}