using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class FieldStartupAnimation : MonoBehaviour
    {
        [SerializeField] private List<Transform> _tiles;

        [SerializeField] private float _scaleDuration = 0.25f;
        [SerializeField] private float _finalScale = 0.97f;
        [SerializeField] private Ease _scaleEase = Ease.OutBounce;

        public IEnumerator Play()
        {
            foreach (var tile in _tiles)
            {
                tile.localScale = Vector3.zero;
            }

            for (int i = 0; i < _tiles.Count; i++)
            {
                var tile = _tiles[i];

                Sequence tileSeq = DOTween.Sequence();

                tileSeq.Append(tile.DOScale(_finalScale, _scaleDuration)
                    .SetEase(_scaleEase));

                yield return null;
            }
        }
    }
}