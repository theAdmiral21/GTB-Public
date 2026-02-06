using System;
using System.Collections.Generic;
using Primitives.Common.State.Enums;
using Primitives.Common.State.Services;
using Primitives.Input;
using Primitives.Unity.Infrastructure;
using Primitives.Execution;
using UnityEngine;
using UnityEngine.InputSystem;
using Primitives.Common.Execution;

namespace PlayerController.Unity.Inputs
{
    public class PlayerActionMapManager : MonoBehaviour, IInitializable<IGameContext>
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private bool _debugStatus;
        public int Priority => 0;
        public InputActionMap CurrentMap => _playerInput.currentActionMap;
        private GameInputs _actionAsset;
        private Dictionary<InputContext, BaseInputReader> _inputReaders = new();
        private IGameStateEvents _stateEvents;

        public event Action<InputContext> OnActionMapChanged;

        private void Awake()
        {
            // Register with the scene boot strapper in order initialize in the correct order
            SceneInitializationRegistry.Register(this);

            _actionAsset = new GameInputs();
            BaseInputReader[] inputReaders = GetComponents<BaseInputReader>();
            // Debug.Log($"Found {inputReaders.Length} input readers");
            foreach (BaseInputReader reader in inputReaders)
            {
                reader.Initialize(_actionAsset);
                _inputReaders[reader.Type] = reader;
                // Debug.Log($"Set action asset for {reader.Type}");
            }


        }

        public void Initialize(IGameContext context)
        {
            _stateEvents = context.GameStateServices.GameStateEvents;
            _stateEvents.OnGameStateChanged += HandleStateChange;
        }
        public void PostInitialize(IGameContext context)
        {
            // After subscribing, make sure you're in the correct state
            HandleStateChange(_stateEvents.CurrentState);
        }

        private void OnDisable()
        {
            _stateEvents.OnGameStateChanged -= HandleStateChange;
        }
        private void HandleStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Booting:
                    {
                        SetActionMap(InputContext.Menu);
                        break;
                    }
                case GameState.Gameplay:
                    {
                        SetActionMap(InputContext.Gameplay);
                        break;
                    }
                case GameState.Paused:
                    {
                        SetActionMap(InputContext.Pause);
                        break;
                    }
            }
        }
        private void Update()
        {
            if (_debugStatus)
            {
                foreach (var reader in _inputReaders.Values)
                {
                    Debug.Log($"{reader.Type} is active: {reader.IsActive}");
                }
            }
        }

        private void DisableAll()
        {
            foreach (BaseInputReader reader in _inputReaders.Values)
            {
                reader.Deactivate();
            }
        }

        public void SetActionMap(InputContext inputContext)
        {
            DisableAll();
            switch (inputContext)
            {
                case InputContext.Gameplay:
                    {
                        _playerInput.SwitchCurrentActionMap("InGame");
                        _inputReaders[InputContext.Gameplay].Activate();
                        break;
                    }
                case InputContext.Menu:
                    {
                        _playerInput.SwitchCurrentActionMap("InMenu");
                        _inputReaders[InputContext.Menu].Activate();
                        break;
                    }
                case InputContext.Conversation:
                    {
                        _playerInput.SwitchCurrentActionMap("InConversation");
                        _inputReaders[InputContext.Conversation].Activate();
                        break;
                    }
                case InputContext.Pause:
                    {
                        _playerInput.SwitchCurrentActionMap("InPause");
                        _inputReaders[InputContext.Pause].Activate();
                        break;
                    }
                case InputContext.CutScene:
                    {
                        _playerInput.SwitchCurrentActionMap("InCutScene");
                        _inputReaders[InputContext.CutScene].Activate();
                        break;
                    }
                case InputContext.None:
                    {
                        Debug.LogError($"{inputContext} was routed to None. Was that on purpose?");
                        break;
                    }
            }
        }
        [ContextMenu("Set Action Map / Gameplay")]
        private void DebugGameplay()
        {
            SetActionMap(InputContext.Gameplay);
            Debug.Log("Switched to Gameplay map");
        }

        [ContextMenu("Set Action Map / Menu")]
        private void DebugMenu()
        {
            SetActionMap(InputContext.Menu);
            Debug.Log("Switched to Menu map");
        }

        [ContextMenu("Set Action Map / Pause")]
        private void DebugPause()
        {
            SetActionMap(InputContext.Pause);
            Debug.Log("Switched to Pause map");
        }

        [ContextMenu("Set Action Map / Conversation")]
        private void DebugConversation()
        {
            SetActionMap(InputContext.Conversation);
            Debug.Log("Switched to Conversation map");
        }

        [ContextMenu("Set Action Map / Cut Scene")]
        private void DebugCutScene()
        {
            SetActionMap(InputContext.CutScene);
            Debug.Log("Switched to CutScene map");
        }


    }
}