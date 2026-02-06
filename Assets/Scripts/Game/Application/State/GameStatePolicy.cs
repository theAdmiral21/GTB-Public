using Game.Application.State.Services;
using Primitives.Common.State.Enums;

namespace Game.Application.State
{
    public class GameStatePolicy : IGameStatePolicy
    {
        public bool CanChange(GameState current, GameState newState)
        {
            return NewStateIsNotCurrentState(current, newState);
        }

        private bool NewStateIsNotCurrentState(GameState current, GameState newState)
        {
            return current != newState;
        }
    }
}