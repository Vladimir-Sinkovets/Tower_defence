using DG.Tweening;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _modelHideAnimationRoot;

        [SerializeField] private float _hideViewDuration = 1.0f;
        [SerializeField] private float _hideViewYPosition = 1.5f;

        private Action _currentAttackCallback;

        public void PlayWalkAnimation() => _animator.SetTrigger(SimpleEnemyAnimationParameters.Walk);

        public void PlayAttackAnimation(Action callback = null)
        {
            _animator.SetTrigger(SimpleEnemyAnimationParameters.Attack);

            _currentAttackCallback = callback;
        }

        public void PlayIdleAnimation() => _animator.SetTrigger(SimpleEnemyAnimationParameters.Idle);

        public void PlayDeathAnimation() => _animator.SetTrigger(SimpleEnemyAnimationParameters.Death);

        public void AttackAnimationEventHandler()
        {
            _currentAttackCallback?.Invoke();

            _currentAttackCallback = null;
        }

        public void DisableCanvas() => _canvas.SetActive(false);

        public void RemoveModel(float delay = 1.0f) => RemoveModelCoroutine(delay, this.GetCancellationTokenOnDestroy()).Forget();

        private async UniTask RemoveModelCoroutine(float delay, CancellationToken ct)
        {
            await UniTask.WaitForSeconds(delay, cancellationToken: ct);

            await _modelHideAnimationRoot.transform.DOMoveY(_modelHideAnimationRoot.transform.position.y - _hideViewYPosition, _hideViewDuration)
                .WithCancellation(ct);
        }

        private void OnDestroy()
        {
            if (_modelHideAnimationRoot != null)
                _modelHideAnimationRoot.transform.DOKill();
        }
    }
}