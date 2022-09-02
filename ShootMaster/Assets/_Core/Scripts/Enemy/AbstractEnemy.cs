using System;
using _Core.Scripts.Weapon.Bullet;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Enemy
{
    [RequireComponent(typeof(Animator))]
    public abstract class AbstractEnemy : MonoBehaviour
    {
        public event Action OnHit;
        [SerializeField] protected int _health = 3;

        protected Animator _animator;
        protected bool _isDead;

        private EnemyController _enemyController;
        private int _wayPointIndex;

        [Inject]
        public void Construct(EnemyController enemyController)
        {
            _enemyController = enemyController;
        }

        public virtual bool GetIsDead() => _isDead;

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AbstractBullet bullet))
                CollisionBullet(bullet);
        }

        public virtual void Start()
        {
            _enemyController.AddAbstractEnemy(this, _wayPointIndex);
            _animator = GetComponent<Animator>();
        }

        public int GetAbstractEnemyHealth() => _health;
        public int SetWayPointIndex(int index) => _wayPointIndex = index;

        protected virtual void ReduceHealth(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                DisableEnemy();
            }
        }

        protected virtual void CollisionBullet(AbstractBullet abstractBullet)
        {
            OnHit.Invoke();
            abstractBullet.DeactivateBullet();
            ReduceHealth(abstractBullet.GetDamage());
        }

        protected virtual void DisableEnemy()
        {
            _enemyController.RemoveAbstractEnemy(this, _wayPointIndex);
        }

        protected virtual void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    break;
                case GameState.WinGame:
                    break;
            }
        }
    }
}