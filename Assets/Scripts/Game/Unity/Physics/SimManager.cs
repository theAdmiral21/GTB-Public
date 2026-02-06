using Primitives.Common.State.Enums;
using Primitives.Common.State.Services;
using UnityEngine;

namespace Primitives.Unity.Physics
{
    public class SimManager
    {

        private IGameStateEvents _gameStateEvents;

        public SimManager(IGameStateEvents gameStateEvents)
        {
            _gameStateEvents = gameStateEvents;

            _gameStateEvents.OnGameStateChanged += ChangeTimeScale;
        }
        private void ChangeTimeScale(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Booting:
                    {
                        Time.timeScale = 0f;
                        break;
                    }
                case GameState.Gameplay:
                    {
                        Time.timeScale = 1f;
                        break;
                    }
                case GameState.Paused:
                    {
                        Time.timeScale = 0f;
                        break;
                    }
            }
        }
    }
}