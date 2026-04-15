using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public static class AnimationExtensions
    {
        private const float FallOffestY = 10f;
        private const float MoveDuration = 0.5f;
        private const float ScaleSqueeze = 1.2f;
        private const float ScaleDownY = 0.1f;
        private const float FinalScale = 1.0f;
        private const float ScaleDuration = 0.15f;

        public static IEnumerator PlayFallDownAppearanceAnimation(this Transform transform)
        {
            var buildingGroundPosition = transform.position;

            transform.position += new Vector3(0, FallOffestY, 0);

            yield return transform.DOMove(buildingGroundPosition, MoveDuration)
                .SetEase(Ease.InExpo).WaitForCompletion();

            transform.DOScaleX(ScaleSqueeze, ScaleDuration)
                .SetEase(Ease.OutBack);

            transform.DOScaleZ(ScaleSqueeze, ScaleDuration)
                .SetEase(Ease.OutBack);

            yield return transform.DOScaleY(ScaleDownY, ScaleDuration)
                .SetEase(Ease.OutBack).WaitForCompletion();

            yield return transform.DOScale(FinalScale, ScaleDuration)
                .SetEase(Ease.OutBack).WaitForCompletion();
        }
    }
}