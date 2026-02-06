

using Primitives.Common.Diagnostics;
using Primitives.Common.Execution;
using Primitives.Common.Infrastructure;
using Primitives.Common.State.Services;

namespace Infrastructure.Application.DataStructures
{
    public class GameContext : IGameContext
    {
        public IDiagnosticSink Diagnostics { get; }
        public IInfrastructureServices QuitServices { get; }
        public IGameStateServices GameStateServices { get; }

        public GameContext(
            IDiagnosticSink diagnostic,
            IGameStateServices gameStateServices,
            IInfrastructureServices quitServices
        )
        {
            Diagnostics = diagnostic;
            GameStateServices = gameStateServices;
            QuitServices = quitServices;
        }
    }
}