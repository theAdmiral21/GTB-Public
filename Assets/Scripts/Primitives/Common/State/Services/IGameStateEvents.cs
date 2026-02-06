using System;
using Primitives.Common.State;
using Primitives.Common.State.Enums;

namespace Primitives.Common.State.Services
{
    public interface IGameStateEvents : IGameStateProvider
    {
        public event Action<GameState> OnGameStateChanged;

    }
}