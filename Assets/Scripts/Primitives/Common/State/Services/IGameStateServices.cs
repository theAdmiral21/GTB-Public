namespace Primitives.Common.State.Services
{
    public interface IGameStateServices
    {
        public IGameStateProvider GameState { get; }
        public IChangeGameStateService ChangeGameState { get; }
        public IGameStateEvents GameStateEvents { get; }
    }
}