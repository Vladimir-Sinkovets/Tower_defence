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
        private Vector3 _startPosition;

        private float _time;
        private float _flightTime;
        private float _arcHeight;
        private ParticleSystem _hitVFXPrefab;

        public void Init(Enemy target, int damage, float speed, float arcHeight, ParticleSystem hitVFXPrefab = null)
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
            _startPosition = transform.position;

            var distance = Vector3.Distance(_startPosition, _targetLastPosition);
            _flightTime = distance / _speed;
            _arcHeight = arcHeight;

            _hitVFXPrefab = hitVFXPrefab;
        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_target != null)
            {
                _targetLastPosition = _target.transform.position;
            }

            var t = _time / _flightTime;

            t = Mathf.Clamp01(t);

            var horizontalPos = Vector3.Lerp(_startPosition, _targetLastPosition, t);

            var height = _arcHeight * 4 * (t - t * t);

            horizontalPos.y += height;

            transform.position = horizontalPos;

            if (!(t >= 1f)) return;
            
            if (_target != null)
                _target.Health.ApplyDamage(_damage);

            Destroy(gameObject);

            if (_hitVFXPrefab == null) return;
            
            var vfx = Instantiate(_hitVFXPrefab, transform.position, Quaternion.identity);
            Destroy(vfx.gameObject, vfx.main.duration);
        }
    }
}