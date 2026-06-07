using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Shared
{
    public class DisposeOnDestroy : MonoBehaviour
    {
        private readonly List<IDisposable> _disposables = new();

        public void Add(params IDisposable[] disposables) => _disposables.AddRange(disposables);

        private void OnDestroy()
        {
            foreach (var d in _disposables)
                d?.Dispose();
        }
    }
}