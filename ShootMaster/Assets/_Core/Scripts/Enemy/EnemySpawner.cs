using System;
using System.Collections.Generic;
using _Core.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<WayPoint> _waypoints;
        private List<EnemyInject> _enemyInject;
        [Inject] private EnemyInject.FactoryEnemyInject factoryEnemyInject;

        private void Start()
        {
            CreateEnemy();
        }

        public EnemySpawner()
        {
            _enemyInject = new List<EnemyInject>();
        }

        public void CreateEnemy()
        {
            for (var i = 0; i < _waypoints.Count; i++)
            {
                var pointsList = _waypoints[i].GetWayPointUnitList();
                _enemyInject.Clear();
                for (var j = 0; j < pointsList.Count; j++)
                {
                    _enemyInject.Add(factoryEnemyInject.Create());
                    var tempEnemyInject = _enemyInject[j].transform;
                    tempEnemyInject.position = pointsList[j].position;
                    _enemyInject[j].SetWayPointIndex(i);
                }
            }
        }

        public List<WayPoint> GetWayPointsList() => _waypoints;
    }
}