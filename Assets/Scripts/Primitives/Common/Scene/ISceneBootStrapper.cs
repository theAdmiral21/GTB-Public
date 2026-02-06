using System;
using Primitives.Common.Execution;
using Primitives.Execution;

namespace Primitives.Application.Scene.Abstractions
{
    public interface ISceneBootStrapper
    {
        public event Action OnReady;
        public void Register(IInitializable<IGameContext> initializable);
        public void InitializeScene();
        public void PostInitializeScene();
    }
}