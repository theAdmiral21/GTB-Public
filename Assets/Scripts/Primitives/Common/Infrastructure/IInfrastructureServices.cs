using Primitives.Infrastructure;

namespace Primitives.Common.Infrastructure
{
    public interface IInfrastructureServices
    {
        public IRequestQuit RequestQuit { get; }
        public IQuitExecutor ExecuteQuit { get; }
    }
}