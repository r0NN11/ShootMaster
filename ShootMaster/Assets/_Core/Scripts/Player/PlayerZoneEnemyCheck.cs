using System.Collections;
using System.Collections.Generic;
using _Core.Scripts.Enemy;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Player
{
    public class PlayerZoneEnemyCheck : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private SphereCollider _sphereCollider;
        [SerializeField] private float _radiusDetect;

        private AbstractEnemy _abstractEnemy;
        private GameStateDirector _gameStateDirector;
        private List<AbstractEnemy> _abstractEnemies;

        [Inject]
        public void Constructor(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
            _gameStateDirector.OnChangeGameState += ChangeGameState;
        }

        private void Start()
        {
            _sphereCollider.radius = _radiusDetect;
            _abstractEnemies = new List<AbstractEnemy>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AbstractEnemy _enemy))
            {
                var abstractEnemy = _enemy;
                var abstractEnemies = _abstractEnemies;
                abstractEnemies.Add(abstractEnemy);
            }
        }

        private void DetermineEnemy()
        {
            float tempMinDistance = _radiusDetect;
            bool isEnemyCheck = false;
            for (var i = 0; i < _abstractEnemies.Count; i++)
            {
                if (_abstractEnemies[i].GetIsDead())
                    continue;

                var distance = Vector3.Distance(transform.position, _abstractEnemies[i].transform.position);
                if (_abstractEnemy != null)
                {
                    if (_abstractEnemy == _abstractEnemies[i])
                        continue;
                }

                if (distance < tempMinDistance)
                {
                    tempMinDistance = distance;
                    _abstractEnemy = _abstractEnemies[i];
                }
            }

            if (_abstractEnemy != null)
            {
                _player.SetAimAbstractEnemy(_abstractEnemy.transform);
            }

            _gameStateDirector.SetGameState(GameState.UpdateEnemyPosition);
        }

        private void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.StartBattle:
                    DetermineEnemy();
                    break;
                case GameState.UpdateEnemy:
                    DetermineEnemy();
                    break;
            }
        }
    }
}