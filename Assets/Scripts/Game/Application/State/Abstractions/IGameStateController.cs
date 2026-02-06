using Primitives.Common.State.Enums;
using Primitives.Common.State.Services;

namespace Game.Application.State.Services
{
    public interface IGameStateController : IGameStateProvider
    {
        public void RequestStateChange(GameState request);
    }
}