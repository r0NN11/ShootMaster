using _Core.Scripts.Weapon.Bullet;
using UnityEngine;

namespace _Core.Scripts.Environment
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class AbstractObject : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AbstractBullet bullet))
                CollisionBullet(bullet);
        }

        public virtual void Start()
        {
            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        protected virtual void CollisionBullet(AbstractBullet abstractBullet)
        {
            abstractBullet.DeactivateBullet();
            _rigidbody.AddForce((abstractBullet.GetDirection()), ForceMode.Impulse);
        }
    }
}