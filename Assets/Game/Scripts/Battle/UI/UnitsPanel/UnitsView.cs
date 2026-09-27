using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Game.Scripts.Battle.UI.UnitsPanel
{
    public class UnitsView : MonoBehaviour, IUnitsView
    {
        public event Action<string> OnOptionChosen;
        
        [SerializeField] private RectTransform _container;
        [SerializeField] private UnitOptionView _unitOptionViewPrefab;
        
        private UnitOptionView _selectedUnitOptionView;
        private List<UnitOptionView> _views;

        public void SetOptions(IEnumerable<UnitOption> units)
        {
            _views = new();
            
            foreach (var unit in units)
            {
                var option = Instantiate(_unitOptionViewPrefab, _container);
                
                option.SetIcon(unit.Icon);
                option.SetTitle(unit.Name);
                option.SetId(unit.Id);
                
                option.OnClicked += OnClickedHandler;
                
                _views.Add(option);
            }
        }

        public void SetDefaultOption() => OnClickedHandler(_views.First());

        private void OnClickedHandler(UnitOptionView unitOptionView)
        {
            if (unitOptionView == null)
                return;
            
            _selectedUnitOptionView?.Deselect();
            
            unitOptionView.Select();
            
            _selectedUnitOptionView = unitOptionView;   
            
            OnOptionChosen?.Invoke(unitOptionView.Id);
        }

        private void OnDestroy()
        {
            foreach (var view in _views)
            {
                view.OnClicked -= OnClickedHandler;
            }
        }
    }
}