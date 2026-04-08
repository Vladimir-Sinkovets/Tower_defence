using UnityEngine;

namespace Assets.Game.Scripts
{
    public class Projectile : MonoBehaviour
    {
        private Health _target;
        private int _damage;
        private float _speed;

        public void Init(Health target, int damage, float speed)
        {
            _target = target;
            _damage = damage;
            _speed = speed;
        }

        private void Update()
        {
            var direction = (_target.transform.position - transform.position).normalized;

            transform.Translate(direction * _speed * Time.deltaTime);

            if (Vector3.Distance(_target.transform.position, transform.position) <= 0.2f)
            {
                _target.GetDamage(_damage);

                Destroy(gameObject);
            }
        }
    }
}