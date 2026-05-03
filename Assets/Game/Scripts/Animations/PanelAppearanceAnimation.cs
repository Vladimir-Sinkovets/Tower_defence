using DG.Tweening;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class PanelAppearanceAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _panel;
        [SerializeField] private SlideDirection _direction = SlideDirection.FromTop;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Ease _ease = Ease.OutCubic;

        private Vector2 _targetPosition;
        private Vector2 _startPosition;

        private void Awake()
        {
            _targetPosition = _panel.anchoredPosition;
            _startPosition = CalculateHiddenPosition();
        }

        public void Show()
        {
            _panel.anchoredPosition = _startPosition;

            _panel.DOKill();

            _panel
                .DOAnchorPos(_targetPosition, _duration)
                .SetEase(_ease)
                .SetUpdate(true);
        }

        public async UniTask Hide(CancellationToken token)
        {
            var hidePosition = CalculateHiddenPosition();

            _panel.DOKill();

            await _panel
                .DOAnchorPos(hidePosition, _duration)
                .SetEase(_ease)
                .SetUpdate(true)
                .WithCancellation(token);
        }

        private Vector2 CalculateHiddenPosition()
        {
            var height = _panel.rect.height;

            var pivotOffset = _direction == SlideDirection.FromTop
                ? (1f - _panel.pivot.y) * height
                : _panel.pivot.y * height;

            var totalOffset = height + pivotOffset;

            var directionMultiplier = _direction == SlideDirection.FromTop ? 1 : -1;

            return _targetPosition + new Vector2(0, totalOffset * directionMultiplier);
        }

        private void OnDestroy() => _panel.DOKill();
    }
    public enum SlideDirection
    {
        FromTop,
        FromBottom
    }
}