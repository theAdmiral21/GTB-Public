using Primitives.Common.State.Enums;

namespace Primitives.Common.State.Services
{
    public interface IGameStateProvider
    {
        public GameState CurrentState { get; }
    }
}