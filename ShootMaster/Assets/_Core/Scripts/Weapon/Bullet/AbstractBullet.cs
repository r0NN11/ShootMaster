using UnityEngine;

namespace _Core.Scripts.Weapon.Bullet
{
    public abstract class AbstractBullet : MonoBehaviour
    {
        [SerializeField] private float _distanceBullet = 10;
        [SerializeField] private float _speedBullet = 50;
        [SerializeField] private int _damage = 1;
        private Vector3 _direction;
        private Vector3 _startPoint;

        private void OnEnable()
        {
            _startPoint = transform.position;
        }

        private void Update()
        {
            var speedBullet = _direction * Time.deltaTime * _speedBullet;
            transform.position += speedBullet;

            if (Vector3.Distance(_startPoint, transform.position) > _distanceBullet)
                DeactivateBullet();
        }

        public virtual void MoveBullet(Vector3 direction)
        {
            gameObject.SetActive(true);
            _direction = direction;
        }

        public virtual int GetDamage() => _damage;
        public virtual Vector3 GetDirection() => _direction * _speedBullet;

        public virtual void DeactivateBullet()
        {
            gameObject.SetActive(false);
        }
    }
}