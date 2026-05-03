using System;
using System.Collections.Generic;

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
        
        
        public void Open(WindowType type)
        {
            if (_activeScreens.TryGetValue(type, out var _))
                return;

            if (!_pool.TryGetValue(type, out var window))
            {
                window = _factory.Create(type);
                _pool.Add(type, window);
            }

            _activeScreens.Add(type, window);
            
            window.Activate();
        }

        public void Close(WindowType type)
        {
            if (!_activeScreens.TryGetValue(type, out var screenEntry)) return;

            screenEntry.Deactivate();
            
            _activeScreens.Remove(type);
        }

        
        public void Dispose()
        {
            foreach (var window in _pool.Values)
            {
                window.Deactivate();
            }
            
            _pool.Clear();
            _activeScreens.Clear();
        }
    }

    public interface IWindowsManager
    {
        public void Open(WindowType type);
        public void Close(WindowType type);
    }
}