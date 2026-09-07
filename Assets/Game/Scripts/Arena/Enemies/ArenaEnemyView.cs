using System;
using Assets.Game.Scripts.Arena.Assets.Game.Scripts.Arena;
using UnityEngine;

namespace Assets.Game.Scripts.Arena
{
    public class ArenaEnemyView : MonoBehaviour
    {
        public event Action OnAttacked;

        [SerializeField] private Animator _animator;

        public void PlayWalkAnimation() => _animator.SetTrigger(ArenaEnemyAnimationParameters.Walk);

        public void PlayAttackAnimation() => _animator.SetTrigger(ArenaEnemyAnimationParameters.Attack);

        public void AttackAnimationEventHandler() => OnAttacked?.Invoke();
    }
}