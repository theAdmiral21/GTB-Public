using Primitives.Common.Diagnostics;
using Primitives.Common.State.Services;
using Primitives.Common.Infrastructure;

namespace Primitives.Common.Execution
{
    public interface IGameContext
    {
        public IDiagnosticSink Diagnostics { get; }

        public IGameStateServices GameStateServices { get; }

        public IInfrastructureServices QuitServices { get; }

    }
}