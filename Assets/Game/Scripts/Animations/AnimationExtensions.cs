using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public static class AnimationExtensions
    {
        public static IEnumerator PlayFallDownAppearanceAnimation(this Transform transform)
        {
            var buildingGroundPosition = transform.transform.position;

            transform.position += new Vector3(0, 10, 0);

            yield return transform.DOMove(buildingGroundPosition, 0.5f)
                .SetEase(Ease.InExpo).WaitForCompletion();

            transform.DOScaleX(1.2f, 0.15f)
                .SetEase(Ease.OutBack);

            transform.DOScaleZ(1.2f, 0.15f)
                .SetEase(Ease.OutBack);

            yield return transform.DOScaleY(0.1f, 0.15f)
                .SetEase(Ease.OutBack).WaitForCompletion();

            yield return transform.DOScale(1.0f, 0.15f)
                .SetEase(Ease.OutBack).WaitForCompletion();
        }
    }
}