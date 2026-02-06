using UnityEngine;
using Primitives.Application.Scene.Abstractions;
using Primitives.Execution;
using System.Collections.Generic;
using Primitives.Common.Execution;

namespace Primitives.Unity.Infrastructure
{
    public static class SceneInitializationRegistry
    {
        private static ISceneBootStrapper _bootStrapper;
        private static List<IInitializable<IGameContext>> _pendingSystems = new();
        public static void SetBootStrapper(ISceneBootStrapper bootStrapper)
        {
            _bootStrapper = bootStrapper;

            foreach (var system in _pendingSystems)
            {
                _bootStrapper.Register(system);
            }
            _pendingSystems.Clear();
        }
        public static void Register(IInitializable<IGameContext> initializable)
        {
            if (_bootStrapper == null)
            {
                _pendingSystems.Add(initializable);
            }
            else
            {
                _bootStrapper.Register(initializable);
            }
        }

        public static void Clear()
        {
            _bootStrapper = null;
            _pendingSystems.Clear();
        }
    }
}