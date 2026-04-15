using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class PanelAppearanceAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private SlideDirection direction = SlideDirection.FromTop;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private Ease ease = Ease.OutCubic;

        private Vector2 targetPosition;
        private Vector2 startPosition;

        private void Awake()
        {
            targetPosition = panel.anchoredPosition;
            startPosition = CalculateHiddenPosition();
        }

        private Vector2 CalculateHiddenPosition()
        {
            var height = panel.rect.height;

            var pivotOffset = direction == SlideDirection.FromTop
                ? (1f - panel.pivot.y) * height
                : panel.pivot.y * height;

            var totalOffset = height + pivotOffset;

            var directionMultiplier = direction == SlideDirection.FromTop ? 1 : -1;

            return targetPosition + new Vector2(0, totalOffset * directionMultiplier);
        }

        public void Show()
        {
            panel.anchoredPosition = startPosition;

            panel.DOKill();

            panel
                .DOAnchorPos(targetPosition, duration)
                .SetEase(ease)
                .SetUpdate(true);
        }

        public void Hide(Action callback = null)
        {
            var hidePosition = CalculateHiddenPosition();

            panel.DOKill();

            panel
                .DOAnchorPos(hidePosition, duration)
                .SetEase(ease)
                .SetUpdate(true)
                .OnComplete(() => callback?.Invoke());
        }

        private void OnDestroy() => panel.DOKill();
    }
    public enum SlideDirection
    {
        FromTop,
        FromBottom
    }
}