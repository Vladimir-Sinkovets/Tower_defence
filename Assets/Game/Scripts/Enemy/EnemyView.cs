using UnityEngine;

namespace Assets.Game.Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void PlayWalkAnimation()
        {
            _animator.SetTrigger("Walk");
        }

        public void PlayAttackAnimation()
        {
            _animator.SetTrigger("Attack");
        }
    }
}