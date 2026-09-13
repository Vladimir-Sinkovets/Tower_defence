using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Arena.UI.Windows.EndGame;
using Assets.Game.Scripts.Arena.UI.Windows.Upgrades;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Arena.UI.Windows
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IInstantiator _instantiator;
        
        private readonly Dictionary<WindowType, Func<IWindowPresenter>> _factoryDelegates;

        public WindowFactory(WindowViewsConfig config, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            
            _factoryDelegates = new Dictionary<WindowType, Func<IWindowPresenter>>
            {
                [WindowType.EndGame] = () => CreateWindow<EndGameView, EndGamePresenter>(config.EndGameViewPrefab),
                [WindowType.Upgrades] = () => CreateWindow<UpgradesView, UpgradesPresenter>(config.UpgradeViewPrefab)
            };
        }

        private IWindowPresenter CreateWindow<TView, TPresenter>(GameObject prefab) where TView : MonoBehaviour
        {
            var view = _instantiator.InstantiatePrefabForComponent<TView>(prefab.GetComponent<TView>());

            var presenter = _instantiator.Instantiate<TPresenter>(new object[] { view });

            return (IWindowPresenter)presenter;
        }

        public IWindowPresenter Create(WindowType type)
        {
            if (_factoryDelegates.TryGetValue(type, out var factory))
                return factory();

            throw new ArgumentOutOfRangeException(nameof(type), type, $"No factory registered for window type {type}");
        }
    }
}