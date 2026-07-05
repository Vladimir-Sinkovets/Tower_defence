using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.UI.Windows
{
    public class WindowsManager : IWindowsManager, IDisposable
    {
        private readonly IWindowFactory _factory;
        
        private readonly Dictionary<WindowType, IWindowPresenter> _activeScreens;
        private readonly Dictionary<WindowType, IWindowPresenter> _pool;

        public WindowsManager(IWindowFactory factory)
        {
            _factory = factory;
            _activeScreens = new Dictionary<WindowType, IWindowPresenter>();
            _pool = new Dictionary<WindowType, IWindowPresenter>();
        }
        
        
        public async UniTask Open(WindowType type)
        {
            if (_activeScreens.TryGetValue(type, out var _))
                return;

            if (!_pool.TryGetValue(type, out var window))
            {
                window = await _factory.Create(type);
                _pool.Add(type, window);
            }

            _activeScreens.Add(type, window);
            
            window.Activate();
        }

        public void Close(WindowType type)
        {
            if (!_activeScreens.TryGetValue(type, out var screenEntry))
                return;

            screenEntry.Deactivate();
            
            _activeScreens.Remove(type);
        }

        public void CloseAll()
        {
            foreach (var screen in _activeScreens.Values)
            {
                screen.Deactivate();
            }
            
            _activeScreens.Clear();
        }
        
        public void Dispose()
        {
            foreach (var window in _activeScreens.Values)
            {
                window.Deactivate();
            }
            
            _pool.Clear();
            _activeScreens.Clear();
        }
    }
}