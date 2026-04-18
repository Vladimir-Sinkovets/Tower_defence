using Assets.Game.Scripts.Animations;
using System;
using System.Collections.Generic;
using Assets.Game.Scripts.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Assets.Game.Scripts.Buildings
{
    public class ChooseBuildingView : MonoBehaviour, IChooseBuildingView
    {
        public event Action<Vector3> OnClicked;
        public event Action<BuildingConfig> OnOptionChosen;
        public event Action OnCloseButtonClicked;

        [SerializeField] private GameObject _panel;
        [SerializeField] private RectTransform _optionsContainer;
        [SerializeField] private BuildingOption _buildingOptionPrefab;
        [SerializeField] private PanelAppearanceAnimation _animation;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _planeCenter;
        [SerializeField] private Transform _pointerPrefab;

        private GameInput _input;
        private Camera _mainCamera;
        
        private readonly List<BuildingOption> _options = new();
        private Transform _pointer;

        [Inject]
        public void Construct(GameInput input)
        {
            _mainCamera = Camera.main;
            _input = input;
        }
        
        private void Awake()
        {
            _input.Touch += OnTouchHandler;
            _closeButton.onClick.AddListener(OnCloseButtonClickedHandler);

            _pointer = Instantiate(_pointerPrefab);
            _pointer.gameObject.SetActive(false);
        }
        
        
        public void Show()
        {
            _panel.SetActive(true);

            if (_animation != null)
                _animation.Show();
        }

        public void Hide()
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

                option.Init(viewModel.Config, viewModel.IsAvailable);

                option.OnClick += OnOptionClickedHandler;

                _options.Add(option);
            }
        }
        
        
        private void OnTouchHandler(Vector2 touchPosition)
        {
            if (IsPointOverUI(touchPosition))
                return;

            var position = GetPoint(touchPosition);
            
            OnClicked?.Invoke(position);
        }

        private void OnCloseButtonClickedHandler() => OnCloseButtonClicked?.Invoke();
        
        private void OnOptionClickedHandler(BuildingOption option) => OnOptionChosen?.Invoke(option.Config);

        private bool IsPointOverUI(Vector2 position)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = position
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            return results.Count > 0;
        }

        private Vector3 GetPoint(Vector2 touchPosition)
        {
            var ray = _mainCamera.ScreenPointToRay(touchPosition);

            var buildPlane = new Plane(Vector3.up, _planeCenter.position);

            if (buildPlane.Raycast(ray, out var enter))
                return ray.GetPoint(enter);

            return Vector3.zero;
        }

        private void ClearContainer()
        {
            if (_options == null)
                return;

            foreach (var option in _options)
            {
                option.OnClick -= OnOptionClickedHandler;
                
                if (option.gameObject)
                    Destroy(option.gameObject);
            }

            _options.Clear();
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClickedHandler);
            _input.Touch -= OnTouchHandler;
            
            ClearContainer();
        }
    }
}