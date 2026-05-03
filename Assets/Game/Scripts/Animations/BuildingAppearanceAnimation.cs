using System.Threading;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class BuildingAppearanceAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _fallOffestY = 10f;
        [SerializeField] private float _moveDuration = 0.5f;
        [SerializeField] private float _scaleSqueeze = 1.2f;
        [SerializeField] private float _scaleDownY = 0.1f;
        [SerializeField] private float _finalScale = 1.0f;
        [SerializeField] private float _scaleDuration = 0.15f;

        public async UniTask Play(CancellationToken ct)
        {
            var buildingGroundPosition = _target.position;

            _target.position += new Vector3(0, _fallOffestY, 0);

            await _target.DOMove(buildingGroundPosition, _moveDuration)
                .SetEase(Ease.InExpo)
                .WithCancellation(ct);

            var scaleXAnimationTask = _target.DOScaleX(_scaleSqueeze, _scaleDuration)
                .SetEase(Ease.OutBack)
                .WithCancellation(ct);

            var scaleZAnimationTask = _target.DOScaleZ(_scaleSqueeze, _scaleDuration)
                .SetEase(Ease.OutBack)
                .WithCancellation(ct);

            var scaleYAnimationTask = _target.DOScaleY(_scaleDownY, _scaleDuration)
                .SetEase(Ease.OutBack)
                .WithCancellation(ct);

            await UniTask.WhenAll(scaleXAnimationTask, scaleZAnimationTask, scaleYAnimationTask);

            await _target.DOScale(_finalScale, _scaleDuration)
                .SetEase(Ease.OutBack)
                .WithCancellation(ct);
        }
    }
}