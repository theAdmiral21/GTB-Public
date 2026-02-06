using Primitives.Common.State.Enums;
using UnityEngine;
using Primitives.Unity.Physics;
using Primitives.Common.State.Services;
using Diagnostics.Unity;
using Infrastructure.Unity;
using Infrastructure.Application.state.Services;
using Infrastructure.Application;
using Infrastructure.Application.DataStructures;
using Primitives.Common.Execution;
using Primitives.Common.Infrastructure;
using Game.Application.State;
using Game.Application.State.Services;
using Game.Unity.Scene;

namespace Game.Unity
{
    /// <summary>
    /// Central orchestrator for all scene systems. This object persists throughout the life of a play session. The GSM instantiates the services contained within GameContext that are then distributed to objects in the scene via scene boot strapper. The GSM also instantiates the SimManager and GameStateManager.
    /// </summary>
    public class GameSystemsManager : MonoBehaviour
    {
        public static GameSystemsManager Instance { get; private set; }
        [SerializeField] SceneBootStrapper _bootStrapper;

        [Header("Game State Contex")]
        [SerializeField] private GameState _currentGameState;
        // Managers
        public GameStateManager GetGameStateManager => _gameStateManager;
        private GameStateManager _gameStateManager;

        public SimManager SimManager => _simManager;
        private SimManager _simManager;

        private IGameStateServices _gameStateServices;
        private IQuitExecutor _executeQuitService;
        private InfrastructureServices _quitServices;
        private IGameContext _gameContext;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"INSTANTIATED GameSystemsManager {GetInstanceID()}");

            // Subscribe to the bootstrapper's ready signal
            _bootStrapper.OnReady += StartScene;

            // Setup your managers
            _gameStateManager = new GameStateManager();
            // Setup your services
            _gameStateServices = new GameStateServices(_gameStateManager);
            _executeQuitService = new ExecuteQuitService();
            _quitServices = new InfrastructureServices(_gameStateManager, _executeQuitService);

            _simManager = new SimManager(_gameStateServices.GameStateEvents);

            _gameContext = new GameContext(
                new UnityDiagnosticSink(),
                _gameStateServices,
                _quitServices
            );

            GameContextRegistry.Set(_gameContext);
        }

        private void StartScene()
        {
            _gameStateServices.ChangeGameState.ChangeGameState(GameState.Gameplay);
        }

        private void OnDestroy()
        {
            Debug.Log($"DESTROYED GameSystemsManager {GetInstanceID()}");
            if (Instance == this)
            {
                _bootStrapper.OnReady -= StartScene;
                Instance = null;
            }
        }

        public void Update()
        {
            if (_gameStateManager != null)
            {
                _currentGameState = _gameStateManager.CurrentState;
            }
        }
    }
}