using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Common.Types
{
    [Serializable]
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        public event Action OnChanged;

        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set
            {
                _dictionary[key] = value;
                OnChanged?.Invoke();
            }
        }

        public ICollection<TKey> Keys => _dictionary.Keys;
        public ICollection<TValue> Values => _dictionary.Values;
        public int Count => _dictionary.Count;
        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
            OnChanged?.Invoke();
        }

        public bool TryAdd(TKey key, TValue value)
        {
            if (!_dictionary.TryAdd(key, value)) return false;
            OnChanged?.Invoke();
            return true;
        }

        public bool Remove(TKey key)
        {
            if (!_dictionary.Remove(key)) return false;
            OnChanged?.Invoke();
            return true;
        }

        public void Clear()
        {
            if (_dictionary.Count == 0) return;
            _dictionary.Clear();
            OnChanged?.Invoke();
        }

        public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);
        public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item.Key, item.Value);
            OnChanged?.Invoke();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) => ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Contains(item);
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(array, arrayIndex);
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Remove(item)) return false;
            OnChanged?.Invoke();
            return true;
        }

        public TValue GetValueOrDefault(TKey key, TValue defaultValue) => _dictionary.GetValueOrDefault(key, defaultValue);
    }
}