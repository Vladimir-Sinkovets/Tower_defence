using DG.Tweening;
using System.Collections;
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
        [SerializeField] private Ease _returnToNoramlEase = Ease.InSine;
        
        public override IEnumerator PlayBeforeAttackAnimation()
        {
            yield return _catapult.DOLocalRotate(_attackRotation, _duration)
                .SetEase(_attackEase)
                .WaitForCompletion();

            _catapult.DOLocalRotate(_normalRotation, _duration)
                .SetEase(_returnToNoramlEase);
        }

        private void OnDestroy()
        {
            if (_catapult != null)
                _catapult.DOKill();
        }
    }
}