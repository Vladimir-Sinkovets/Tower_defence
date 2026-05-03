using System.Threading;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class BarrelAnimation : WeaponAnimation
    {
        [SerializeField] private Transform _barrel;

        [SerializeField] private float _duration = 0.1f;
        [SerializeField] private float _squeezeZ = 0.6f;
        [SerializeField] private Ease _squeezeZEase = Ease.InOutSine;
        [SerializeField] private float _squeezeX = 1.15f;
        [SerializeField] private Ease _squeezeXEase = Ease.InOutSine;
        [SerializeField] private float _squeezeY = 1.15f;
        [SerializeField] private Ease _squeezeYEase = Ease.InOutSine;
        [SerializeField] private float _finalScale = 1.0f;
        [SerializeField] private Ease _returnToNormalScaleEase = Ease.InQuint;

        public override async UniTask PlayBeforeAttackAnimation(CancellationToken ct)
        {
            await DOTween.Sequence()
                .Append(_barrel.DOScaleZ(_squeezeZ, _duration)
                    .SetEase(_squeezeZEase))
                .Join(_barrel.DOScaleY(_squeezeY, _duration)
                    .SetEase(_squeezeYEase))
                .Join(_barrel.DOScaleX(_squeezeX, _duration)
                    .SetEase(_squeezeXEase))
                .WithCancellation(ct);

            await _barrel.DOScale(_finalScale, _duration)
                .SetEase(_returnToNormalScaleEase)
                .WithCancellation(ct);
        }

        private void OnDestroy()
        {
            if (_barrel != null)
                _barrel.DOKill();
        }
    }
}