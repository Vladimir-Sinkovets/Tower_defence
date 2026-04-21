using Assets.Game.Scripts.Animations;
using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Input;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Game.Scripts.UI.Buildings
{
    public class ChooseBuildingView : MonoBehaviour, IChooseBuildingView
    {
        public event Action<Vector3> OnClicked;
        public event Action<int> OnOptionChosen;
        public event Action OnCloseButtonClicked;

        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _optionsContainer;
        [SerializeField] private BuildingOption _buildingOptionPrefab;
        [SerializeField] private PanelAppearanceAnimation _animation;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _pointerPrefab;
        
        private readonly List<BuildingOption> _options = new();
        private Transform _pointer;
        
        private PointSelector _pointSelector;

        [Inject]
        private void Construct(PointSelector pointSelector)
        {
            _pointSelector = pointSelector;
        }
        
        private void Awake()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClickedHandler);
            _pointSelector.OnClicked += OnPointSelectedHandler;

            _pointer = Instantiate(_pointerPrefab);
            _pointer.gameObject.SetActive(false);
        }

        public void ShowPanel()
        {
            _panel.SetActive(true);

            if (_animation != null)
                _animation.Show();
        }

        public void HidePanel()
        {
            if (_animation != null)
                _animation.Hide(() =>
                {
                    _panel.SetActive(false);
                });
            else
            {
                _panel.SetActive(false);
            }
        }

        public void ShowPointer(Vector3 position)
        {
            _pointer.transform.position = position;
            _pointer.gameObject.SetActive(true);
        }

        public void HidePointer() => _pointer.gameObject.SetActive((false));

        public void Render(List<BuildingOptionViewModel> viewModels)
        {
            ClearContainer();

            foreach (var viewModel in viewModels)
            {
                var option = Instantiate(_buildingOptionPrefab, _optionsContainer);

                option.Init(viewModel.Icon, viewModel.Index, viewModel.Price, viewModel.IsAvailable);

                option.OnClick += OnOptionClickedHandler;

                _options.Add(option);
            }
        }

        private void OnPointSelectedHandler(Vector3 point) => OnClicked?.Invoke(point);
        
        private void OnOptionClickedHandler(BuildingOption option) => OnOptionChosen?.Invoke(option.Index);

        private void OnCloseButtonClickedHandler() => OnCloseButtonClicked?.Invoke();
        
        private void ClearContainer()
        {
            if (_options == null)
                return;

            foreach (var option in _options)
            {
                option.OnClick -= OnOptionClickedHandler;
                
                if (option.gameObject != null)
                    Destroy(option.gameObject);
            }

            _options.Clear();
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClickedHandler);
            _pointSelector.OnClicked -= OnPointSelectedHandler;
            
            ClearContainer();
        }
    }
}