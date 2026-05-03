using DG.Tweening;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class FieldStartupAnimation : MonoBehaviour
    {
        [SerializeField] private List<Transform> _tiles;

        [SerializeField] private float _scaleDuration = 0.25f;
        [SerializeField] private float _finalScale = 0.97f;
        [SerializeField] private Ease _scaleEase = Ease.OutBounce;

        public async UniTask Play(CancellationToken ct)
        {
            foreach (var tile in _tiles)
            {
                tile.localScale = Vector3.zero;
            }

            var tileAnimations = new List<UniTask>();
            
            foreach (var tile in _tiles)
            {
                var task = tile.DOScale(_finalScale, _scaleDuration)
                    .SetEase(_scaleEase)
                    .WithCancellation(ct);

                tileAnimations.Add(task);
                
                await UniTask.NextFrame(ct);
            }
            
            await UniTask.WhenAll(tileAnimations);
        }
    }
}