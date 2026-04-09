using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class BarrelAnimation : WeaponAnimation
    {
        [SerializeField] private Transform _barrel;

        [SerializeField] private float _duration = 0.1f;

        public override IEnumerator PlayBeforeAttackAnimation()
        {
            yield return DOTween.Sequence()
                .Append(_barrel.DOScaleZ(0.6f, _duration)
                    .SetEase(Ease.InOutSine))
                .Append(_barrel.DOScaleY(1.15f, _duration)
                    .SetEase(Ease.InOutSine))
                .Append(_barrel.DOScaleX(1.15f, _duration)
                    .SetEase(Ease.InOutSine))
                .WaitForCompletion();

            yield return _barrel.DOScale(1.0f, _duration)
                .SetEase(Ease.InQuint)
                .WaitForCompletion();
        }
    }
}