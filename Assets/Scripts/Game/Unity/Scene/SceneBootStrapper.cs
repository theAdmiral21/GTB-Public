using System.Collections.Generic;
using System.Linq;
using System.Text;
using Primitives.Application.Scene.Abstractions;
using Primitives.Unity.Infrastructure;
using Infrastructure.Unity;
using log4net.Util;
using Primitives.Execution;
using UnityEngine;
using Primitives.Common.Execution;
using System;

namespace Game.Unity.Scene
{
    /// <summary>
    /// Boot strapper used to prepare a given scene. The boot strapper uses the GameContextRegistry to initialize subscribed objects within the scene. Objects are initialized in two passes, Initialize and Post Initialize, to prevent race conditions.
    /// </summary>
    public class SceneBootStrapper : MonoBehaviour, ISceneBootStrapper
    {

        [SerializeField] private bool _printDebug;
        private List<IInitializable<IGameContext>> _systems = new();
        private IGameContext _gameContext;
        public event Action OnReady;

        public void Awake()
        {
            SceneInitializationRegistry.SetBootStrapper(this);
        }

        private void Start()
        {
            _gameContext = GameContextRegistry.GameContext;
            if (_gameContext == null)
            {
                Debug.LogError($"Game context is null. Check the GameSystemsManager.");
                return;
            }

            // Order initialization order by priority.
            _systems = _systems.OrderBy(s => s.Priority).ToList();

            if (_printDebug)
            {
                PrintDebug();
            }


            // Do I call this here or some where else? 
            InitializeScene();


            PostInitializeScene();

            // Notify the GSM that we're ready to start
            OnReady?.Invoke();
        }

        private void OnDestroy()
        {
            SceneInitializationRegistry.Clear();
        }

        public void Register(IInitializable<IGameContext> system)
        {
            _systems.Add(system);
            Debug.Log($"{name} registered {system}");
        }

        public void InitializeScene()
        {
            foreach (var system in _systems)
            {
                system.Initialize(_gameContext);
            }
        }

        public void PostInitializeScene()
        {
            foreach (var system in _systems)
            {
                system.PostInitialize(_gameContext);
            }
        }

        private void PrintDebug()
        {
            StringBuilder _debugSb = new StringBuilder();
            _debugSb.AppendLine("Initialization order:");
            for (int i = 0; i < _systems.Count; i++)
            {
                string msg = $"{i + 1}. {_systems[i]} priority: {_systems[i].Priority}";
                _debugSb.AppendLine(msg);
            }
            Debug.Log(_debugSb);
        }
    }
}