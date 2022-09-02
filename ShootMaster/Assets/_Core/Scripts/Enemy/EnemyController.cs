using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Core.Scripts.Enemy
{
    public class EnemyController
    {
        private GameStateDirector _gameStateDirector;
        private EnemySpawner _enemySpawner;

        [Inject]
        public void Construct(GameStateDirector gameStateDirector, EnemySpawner enemySpawner)
        {
            _gameStateDirector = gameStateDirector;
            _enemySpawner = enemySpawner;
        }

        public void AddAbstractEnemy(AbstractEnemy abstractEnemy, int wayPointIndex)
        {
            var enemiesList = _enemySpawner.GetWayPointsList()[wayPointIndex].GetAbstractEnemiesList;
            enemiesList.Add(abstractEnemy);
        }

        public void RemoveAbstractEnemy(AbstractEnemy abstractEnemy, int wayPointIndex)
        {
            var enemiesList = _enemySpawner.GetWayPointsList()[wayPointIndex].GetAbstractEnemiesList;
            enemiesList.Remove(abstractEnemy);
            if (enemiesList.Count != 0)
                _gameStateDirector.SetGameState(GameState.UpdateEnemy);
        }
    }
}