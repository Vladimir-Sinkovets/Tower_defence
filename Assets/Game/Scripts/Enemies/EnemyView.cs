using System;
using UnityEngine;

namespace Assets.Game.Scripts.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private Action _currentAttackCallback;

        public void PlayWalkAnimation()
        {
            _animator.SetTrigger("Walk");
        }

        public void PlayAttackAnimation(Action callback = null)
        {
            _animator.SetTrigger("Attack");

            _currentAttackCallback = callback;
        }

        public void PlayIdleAnimation()
        {
            _animator.SetTrigger("Idle");
        }

        public void PlayDeathAnimation()
        {
            _animator.SetTrigger("Death");
        }

        public void AttckAnimationEventHandler()
        {
            _currentAttackCallback?.Invoke();

            _currentAttackCallback = null;
        }
    }
}