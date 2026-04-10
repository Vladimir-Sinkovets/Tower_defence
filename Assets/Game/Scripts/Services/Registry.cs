using System.Collections.Generic;

namespace Assets.Game.Scripts.Services
{
    public class Registry<T> where T : class
    {
        private readonly HashSet<T> _hashset = new();

        public IEnumerable<T> All => _hashset;
        public bool Register(T item) => _hashset.Add(item);
        public bool Unregister(T item) => _hashset.Remove(item);
    }
}