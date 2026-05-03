using System.Threading;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class CatapultAnimation : WeaponAnimation
    {
        [SerializeField] private Transform _catapult;

        [SerializeField] private float _duration = 0.1f;
        [SerializeField] private Vector3 _attackRotation = new (60f, 0f, 0f);
        [SerializeField] private Ease _attackEase = Ease.InElastic;
        [SerializeField] private Vector3 _normalRotation = Vector3.zero;
        [SerializeField] private Ease _returnToNormalEase = Ease.InSine;
        
        public override async UniTask PlayBeforeAttackAnimation(CancellationToken ct)
        {
            await _catapult.DOLocalRotate(_attackRotation, _duration)
                .SetEase(_attackEase)
                .WithCancellation(ct);

            _catapult.DOLocalRotate(_normalRotation, _duration)
                .SetEase(_returnToNormalEase)
                .WithCancellation(ct)
                .Forget();
        }

        private void OnDestroy()
        {
            if (_catapult != null)
                _catapult.DOKill();
        }
    }
}