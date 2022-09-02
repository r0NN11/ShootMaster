using UnityEngine;
using Zenject;

namespace _Core.Scripts.Environment
{
    public class GameStateChanger : MonoBehaviour
    {
        private GameStateDirector _gameStateDirector;

        [Inject]
        public void Construct(GameStateDirector gameStateDirector)
        {
            _gameStateDirector = gameStateDirector;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                if (player.LastWayPoint())
                    _gameStateDirector.SetGameState(GameState.WinGame);
                else
                    _gameStateDirector.SetGameState(GameState.StartBattle);
        }
    }
}