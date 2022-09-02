using System;
using System.Collections.Generic;
using _Core.Scripts.Enemy;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Player
{
    public class WayPoint : MonoBehaviour
    {
        public List<AbstractEnemy> enemiesList;

        [Header("Put enemies positions for this waypoint")] [SerializeField]
        private List<Transform> _listUnitPosition;

        private GameStateDirector _gameStateDirector;

        [Inject]
        public void Construct(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
        }

        public WayPoint()
        {
            enemiesList = new List<AbstractEnemy>();
        }

        private void Update()
        {
            if (_listUnitPosition.Count != 0)
            {
                if (enemiesList.Count == 0 & _gameStateDirector.GetCurrentGameState() != GameState.EndBattle &
                    _gameStateDirector.GetCurrentGameState() != GameState.Start)
                {
                    _gameStateDirector.SetGameState(GameState.EndBattle);
                    gameObject.SetActive(false);
                }
            }
        }

        public List<Transform> GetWayPointUnitList() => _listUnitPosition;
        public List<AbstractEnemy> GetAbstractEnemiesList => enemiesList;
    }
}