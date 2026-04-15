using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _modelHideAnimationRoot;

        [SerializeField] private float HideViewDuration = 1.0f;
        [SerializeField] private float HideViewYPosition = 1.5f;

        private Action _currentAttackCallback;

        public void PlayWalkAnimation() => _animator.SetTrigger(SimpleEnemyAnimationParameters.Walk);

        public void PlayAttackAnimation(Action callback = null)
        {
            _animator.SetTrigger(SimpleEnemyAnimationParameters.Attack);

            _currentAttackCallback = callback;
        }

        public void PlayIdleAnimation() => _animator.SetTrigger(SimpleEnemyAnimationParameters.Idle);

        public void PlayDeathAnimation() => _animator.SetTrigger(SimpleEnemyAnimationParameters.Death);

        public void AttckAnimationEventHandler()
        {
            _currentAttackCallback?.Invoke();

            _currentAttackCallback = null;
        }

        public void DisableCanvas() => _canvas.SetActive(false);

        public void EnableCanvas() => _canvas.SetActive(true);

        public void RemoveModel(float delay = 1.0f) => StartCoroutine(RemoveModelCoroutine(delay));

        private IEnumerator RemoveModelCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            yield return _modelHideAnimationRoot.transform.DOMoveY(_modelHideAnimationRoot.transform.position.y - HideViewYPosition, HideViewDuration);
        }

        private void OnDestroy() => _modelHideAnimationRoot.transform?.DOKill();
    }
}