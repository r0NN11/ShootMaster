using System;
using UnityEngine;

namespace _Core.Scripts.UI
{
    public class CanvasStart : CanvasAbstract
    {
        public void SetStart()
        {
            _gameStateDirector.SetGameState(GameState.StartGame);
        }

        private void SetCanvasState(bool state)
        {
            gameObject.SetActive(state);
        }

        protected override void ChangeGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Start:
                    SetCanvasState(true);
                    break;
                case GameState.StartGame:
                    SetCanvasState(false);
                    break;
            }
        }
    }
}