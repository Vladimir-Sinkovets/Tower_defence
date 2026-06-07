using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Services.Registries
{
    public class Registry<T> where T : class
    {
        private readonly HashSet<T> _hashset = new();

        public event Action<T> OnRegistered;
        public event Action<T> OnUnregistered;
        
        public IEnumerable<T> All => _hashset;
        public bool Register(T item)
        {
            var isSuccess = _hashset.Add(item);
            
            if (!isSuccess)
                return false;
            
            OnRegistered?.Invoke(item);
            
            return true;
        }

        public bool Unregister(T item)
        {
            var isSuccess = _hashset.Remove(item);
            
            if (!isSuccess)
                return false;
            
            OnUnregistered?.Invoke(item);
            
            return true;
        }
    }
}