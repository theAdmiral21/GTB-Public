using Primitives.Application.Scene.Abstractions;
using Primitives.Common.Execution;

namespace Primitives.Execution
{
    public interface ISceneRegistry
    {
        public void Register(IInitializable<IGameContext> initializable);

        public void SetBootStrapper(ISceneBootStrapper bootStrapper);
    }
}