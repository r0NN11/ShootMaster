using System.Collections.Generic;
using _Core.Scripts.Weapon.Bullet;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Weapon
{
    public abstract class AbstractWeapon : MonoBehaviour
    {
        [SerializeField] private AbstractBullet _abstractBullet;
        [SerializeField] private Transform _pointSpawnBullet;
        [SerializeField] private Transform _pointDistanceCheck;
        protected BulletPoolManager _bulletPoolManager;
        protected const string LAYER_ENEMY = "Enemy";
        private Camera _mainCamera;


        [Inject]
        public void Construct(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        private void Awake()
        {
            _bulletPoolManager = new BulletPoolManager(_abstractBullet);
        }

        public virtual void Shot()
        {
            var abstractBullet = _bulletPoolManager.GetBullet();
            var pos = Input.mousePosition;
            var screenPointToRay = _mainCamera.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));
            abstractBullet.gameObject.SetActive(true);
            abstractBullet.transform.position = _pointSpawnBullet.position;

            if (Physics.Raycast(screenPointToRay, out RaycastHit hit, 1000, ~LayerMask.GetMask("PlayerZoneEnemyCheck")))
            {
                Vector3 shootDirection = (hit.point - _pointSpawnBullet.position).normalized;
                abstractBullet.MoveBullet(shootDirection);
            }
        }

        protected sealed class BulletPoolManager
        {
            private const int COUNT_BULLET_START_POOL_MANAGER = 50;

            private readonly List<AbstractBullet> _abstractBullets;
            private readonly AbstractBullet _abstractBullet;


            public BulletPoolManager(AbstractBullet abstractBullet)
            {
                _abstractBullet = abstractBullet;
                _abstractBullets = new List<AbstractBullet>(COUNT_BULLET_START_POOL_MANAGER);

                BulletCreator(COUNT_BULLET_START_POOL_MANAGER);
            }

            public AbstractBullet GetBullet()
            {
                for (var i = 0; i < _abstractBullets.Count; i++)
                {
                    if (!_abstractBullets[i].gameObject.activeSelf)
                    {
                        return _abstractBullets[i];
                    }
                }

                BulletCreator(COUNT_BULLET_START_POOL_MANAGER / 2);
                return GetBullet();
            }

            private void BulletCreator(int countBullet)
            {
                while (countBullet > 0)
                {
                    _abstractBullets.Add(Object.Instantiate(_abstractBullet));
                    _abstractBullets[_abstractBullets.Count - 1].gameObject.SetActive(false);
                    countBullet--;
                }
            }
        }
    }
}