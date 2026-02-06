using System;
using Game.Application.State.Services;
using Primitives.Common.State.Enums;
using Primitives.Common.State.Services;
using Primitives.Infrastructure;
using UnityEngine;

namespace Game.Application.State
{
    public class GameStateManager : IGameStateController, IGameStateEvents, IQuitApprover
    {
        public GameState CurrentState => _currentState;
        private GameState _currentState;
        private GameStatePolicy _statePolicy = new GameStatePolicy();
        public event Action<GameState> OnGameStateChanged;



        public void RequestStateChange(GameState newState)
        {
            if (_statePolicy.CanChange(_currentState, newState))
            {
                Debug.Log($"Changed game state from {_currentState} to {newState}");
                SetGameState(newState);
            }
        }

        private void SetGameState(GameState newState)
        {
            _currentState = newState;
            OnGameStateChanged?.Invoke(_currentState);
        }

        public bool CanQuit()
        {
            // Evaluate if we can quit the game
            if (_currentState == GameState.Paused) return true;
            if (_currentState == GameState.InMenu) return true;
            return false;
        }
    }
}