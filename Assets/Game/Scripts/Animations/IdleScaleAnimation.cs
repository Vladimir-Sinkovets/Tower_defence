using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class IdleScaleAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _scale = 1.1f;
        [SerializeField] private float _duration = 1.5f;
        [SerializeField] private Ease _ease = Ease.InSine;

        private Vector3 _initialScale;
        private Tween _tween;

        private void Start()
        {
            _initialScale = _target.localScale;

            _tween = _target.transform.DOScale(_scale, _duration)
                .SetEase(_ease)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            _tween?.Kill();

            if (_target != null)
                _target.localScale = _initialScale;
        }
    }
}