using System;
using Game.Application.State;
using Primitives.Common.State.Enums;
using Primitives.Common.State.Services;

namespace Game.Application.State.Services
{
    public class GameStateEventService : IGameStateEvents
    {
        public GameState CurrentState => _gameStateManager.CurrentState;
        private GameStateManager _gameStateManager;

        public GameStateEventService(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public event Action<GameState> OnGameStateChanged
        {
            add => _gameStateManager.OnGameStateChanged += value;
            remove => _gameStateManager.OnGameStateChanged -= value;
        }
    }
}