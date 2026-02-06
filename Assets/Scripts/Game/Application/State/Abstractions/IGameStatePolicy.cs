using Primitives.Common.State.Enums;

namespace Game.Application.State.Services
{
    public interface IGameStatePolicy
    {
        public bool CanChange(GameState current, GameState newState);
    }
}