using Primitives.Common.State.Enums;

namespace Primitives.Common.State.Services
{
    public interface IChangeGameStateService
    {
        public void ChangeGameState(GameState newState);

    }
}