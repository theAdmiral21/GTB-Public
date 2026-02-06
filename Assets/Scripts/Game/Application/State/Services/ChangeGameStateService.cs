using Primitives.Common.State.Enums;
using Primitives.Common.State.Services;

namespace Game.Application.State.Services
{
    public class ChangeGameStateService : IChangeGameStateService
    {
        private readonly GameStateManager _gameStateManager;

        public ChangeGameStateService(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }

        public void ChangeGameState(GameState newState)
        {
            _gameStateManager.RequestStateChange(newState);
        }
    }
}