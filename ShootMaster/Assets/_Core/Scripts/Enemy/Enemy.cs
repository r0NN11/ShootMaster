using _Core.Scripts.Weapon.Bullet;
using UnityEngine;

namespace _Core.Scripts.Enemy
{
    public class Enemy : AbstractEnemy
    {
        public override void Start()
        {
            base.Start();
            SetRigidbodyKinematic(true);
            SetColliderEnablement(false);
        }

        protected override void DisableEnemy()
        {
            base.DisableEnemy();
            _isDead = true;
            _animator.enabled = false;
            SetRigidbodyKinematic(false);
            SetColliderEnablement(true);
        }

        private void SetRigidbodyKinematic(bool state)
        {
            Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rg in rigidbodies)
            {
                rg.isKinematic = state;
            }
        }

        private void SetColliderEnablement(bool state)
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider cr in colliders)
            {
                cr.enabled = state;
            }

            GetComponent<Collider>().enabled = !state;
        }
    }
}