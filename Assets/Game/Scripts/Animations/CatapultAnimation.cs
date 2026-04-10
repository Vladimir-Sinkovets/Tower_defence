using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class CatapultAnimation : WeaponAnimation
    {
        [SerializeField] private Transform _catapult;

        [SerializeField] private float _duration = 0.1f;

        public override IEnumerator PlayBeforeAttackAnimation()
        {
            Debug.Log("Catapult");

            yield return _catapult.DOLocalRotate(new Vector3(60, 0, 0), _duration)
                .SetEase(Ease.InElastic)
                .WaitForCompletion();

            _catapult.DOLocalRotate(new Vector3(0, 0, 0), _duration)
                .SetEase(Ease.InSine);
        }
    }
}