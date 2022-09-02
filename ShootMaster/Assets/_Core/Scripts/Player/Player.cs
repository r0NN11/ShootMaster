using System;
using System.Collections.Generic;
using _Core.Scripts.Enemy;
using _Core.Scripts.Weapon;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using DG.Tweening;

namespace _Core.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private AbstractWeapon _abstractWeapon;
        [SerializeField] private Animator _animator;

        private static readonly int Move = Animator.StringToHash("Move");

        private List<WayPoint> _wayPoints;
        private int _nextWayPointIndex = 0;
        private GameStateDirector _gameStateDirector;
        private EnemySpawner _enemySpawner;
        private float _minimumDistance = 20;
        private Transform _abstractEnemyAim;

        [Inject]
        public void Construct(GameStateDirector gameStateDirector, EnemySpawner enemySpawner)
        {
            _gameStateDirector = gameStateDirector;
            _enemySpawner = enemySpawner;
            _gameStateDirector.OnChangeGameState += ChangeGameState;
        }

        public bool LastWayPoint() => _nextWayPointIndex == _wayPoints.Count - 1;

        public void SetAimAbstractEnemy(Transform abstractEnemy)
        {
            _abstractEnemyAim = abstractEnemy;
        }

        private void Start()
        {
            _wayPoints = _enemySpawner.GetWayPointsList();
            _gameStateDirector.SetGameState(GameState.Start);
        }

        private void Update()
        {
            if (_gameStateDirector.GetCurrentGameState() == GameState.StartBattle ||
                _gameStateDirector.GetCurrentGameState() == GameState.UpdateEnemy)
            {
                if ((Input.GetMouseButtonDown(0)))
                {
                    _abstractWeapon.Shot();
                }
            }
        }

        private void GoToNextWayPoint()
        {
            _animator.SetBool(Move, true);
            if (_wayPoints.Count == 0)
            {
                return;
            }

            _nextWayPointIndex++;
            _navMeshAgent.SetDestination(_wayPoints[_nextWayPointIndex].transform.position);
        }

        private void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartGame:
                    GoToNextWayPoint();
                    break;
                case GameState.StartBattle:
                    _navMeshAgent.enabled = false;
                    _animator.SetBool(Move, false);
                    break;
                case GameState.UpdateEnemyPosition:
                    RotationToAbstractEnemy();
                    break;
                case GameState.EndBattle:
                    _navMeshAgent.enabled = true;
                    GoToNextWayPoint();
                    break;
                case GameState.WinGame:
                    _animator.SetBool(Move, false);
                    break;
            }
        }

        private void RotationToAbstractEnemy()
        {
            if (_abstractEnemyAim != null)
            {
                var transformPosition = -(transform.position - _abstractEnemyAim.position);
                transformPosition = new Vector3(transformPosition.x, 0, transformPosition.z);
                transform.forward = transformPosition;
            }
        }
    }
}