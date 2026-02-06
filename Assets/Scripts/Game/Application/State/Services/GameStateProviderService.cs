using Primitives.Common.State.Enums;
using Primitives.Common.State.Services;

namespace Game.Application.State.Services
{
    public class GameStateProviderService : IGameStateProvider
    {
        public GameState CurrentState => _gameStateManager.CurrentState;
        private GameStateManager _gameStateManager;
        public GameStateProviderService(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }
    }
}