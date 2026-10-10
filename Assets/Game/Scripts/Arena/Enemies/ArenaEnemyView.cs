using System;
using Assets.Game.Scripts.Arena.Enemies;
using Assets.Game.Scripts.Enemies;
using UnityEngine;

namespace Assets.Game.Scripts.Arena
{
    public class ArenaEnemyView : MonoBehaviour
    {
        public event Action OnAttacked;

        [SerializeField] private Animator _animator;
        [SerializeField] private EnemyAnimationEventHandler _handler;

        public void PlayWalkAnimation() => _animator.SetTrigger(ArenaEnemyAnimationParameters.Walk);

        public void PlayAttackAnimation() => _animator.SetTrigger(ArenaEnemyAnimationParameters.Attack);

        private void Awake() => _handler.OnHit += AttackAnimationEventHandler;

        private void AttackAnimationEventHandler() => OnAttacked?.Invoke();

        private void OnDestroy() => _handler.OnHit -= AttackAnimationEventHandler;
    }
}