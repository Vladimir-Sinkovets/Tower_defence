using Assets.Game.Scripts.Enemies;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class Projectile : MonoBehaviour
    {
        private Enemy _target;
        private int _damage;
        private float _speed;

        private Vector3 _targetLastPosition;

        public void Init(Enemy target, int damage, float speed)
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            _target = target;
            _damage = damage;
            _speed = speed;

            _targetLastPosition = target.transform.position;
        }

        private void Update()
        {
            Vector3 target;

            if (_target != null)
            {
                target = _target.transform.position;
                _targetLastPosition = target;
            }
            else
            {
                target = _target.transform.position;
            }

            var direction = (target - transform.position).normalized;

            transform.Translate(_speed * Time.deltaTime * direction);

            if (Vector3.Distance(target, transform.position) <= 0.2f)
            {
                if (_target != null)
                    _target.Health.ApplyDamage(_damage);

                Destroy(gameObject);
            }
        }
    }
}