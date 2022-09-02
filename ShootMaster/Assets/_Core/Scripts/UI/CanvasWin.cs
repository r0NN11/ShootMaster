using UnityEngine;

namespace _Core.Scripts.UI
{
    public class CanvasWin : CanvasAbstract
    {
        public void RestartLevel()
        {
            SceneController.Instance.RestartScene();
        }

        protected override void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.WinGame:
                    gameObject.SetActive(true);
                    break;
            }
        }
    }
}