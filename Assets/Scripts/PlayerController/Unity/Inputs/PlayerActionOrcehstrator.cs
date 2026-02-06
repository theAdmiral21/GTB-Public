using System.Collections.Generic;
using System.Text;
using PlayerController.Application;
using PlayerController.Application.Abstractions;
using PlayerController.Application.Movement;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Application.Movement.Services;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Unity.Animations;
using PlayerController.Unity.Effects;
using PlayerController.Unity.Physics;
using Primitives.Common.State.Enums;
using Primitives.Common.State.Services;
using UnityEngine;
using Primitives.Execution;
using Primitives.Unity.Infrastructure;
using Primitives.Common.Execution;

namespace PlayerController.Unity.Inputs
{
    public class PlayerActionOrchestrator : MonoBehaviour, IInitializable<IGameContext>
    {
        [SerializeField] private PlayerInputReader _inputReader;
        [SerializeField] private PhysicsMonitor _physicsMonitor;
        [SerializeField] private PlayerSimulationDriver _simulationDriver;
        [SerializeField] private PlayerEffectOrchestrator _effectOrchestrator;
        [SerializeField] private AnimatorEffectBridge _animatorBridge;
        public int Priority => 2;

        [Header("Debug contex")]
        [SerializeField] private PlayerActionContext _debugContext;

        private List<IActionRequest> _requests = new List<IActionRequest>();
        private ActionDispatcher _actionDispatch;
        private AppOrchestrator _appOrchestrator;
        private PlayerRuleState _ruleState;
        private InputState _inputState;
        private IGameStateProvider _gameState;
        private IChangeGameStateService _changeGameState;

        // Debug fields
        private StringBuilder _debugSb = new StringBuilder();

        private void Awake()
        {
            // Register with the scene boot strapper in order initialize in the correct order
            SceneInitializationRegistry.Register(this);

            _actionDispatch = new ActionDispatcher(BuildActionDispatchers());

            _appOrchestrator = new AppOrchestrator(_actionDispatch);

            _inputState = new InputState();

        }

        public void Initialize(IGameContext context)
        {
            _gameState = context.GameStateServices.GameState;
            _changeGameState = context.GameStateServices.ChangeGameState;
        }
        public void PostInitialize(IGameContext context)
        {
            _ruleState = new PlayerRuleState(_simulationDriver.Stats);
            _effectOrchestrator.SetPlayerStats(_simulationDriver.Stats);
        }

        private void OnEnable()
        {
            // Actions
            _inputReader.JumpCancel += UpdateRequestList;
            _inputReader.Run += UpdateRequestList;
            _inputReader.RunStop += UpdateRequestList;
            _inputReader.QuickStep += UpdateRequestList;
            _inputReader.Bark += UpdateRequestList;
            _inputReader.Pause += UpdateRequestList;

            // Buffered actions
            _inputReader.BufferJump += BufferInputs;
        }

        private void OnDisable()
        {
            // Actions
            _inputReader.JumpCancel -= UpdateRequestList;
            _inputReader.Run -= UpdateRequestList;
            _inputReader.RunStop -= UpdateRequestList;
            _inputReader.QuickStep -= UpdateRequestList;
            _inputReader.Bark -= UpdateRequestList;
            _inputReader.Pause -= UpdateRequestList;

            // Buffered actions
            _inputReader.BufferJump -= BufferInputs;
        }

        private void Update()
        {
            PhysicsContext physicsContext = _physicsMonitor.CurrentContext;
            PlayerActionContext context = new PlayerActionContext
            {
                CurrentRequests = _requests,
                Facts = physicsContext,
                RuleState = _ruleState,
                CurrentGameState = _gameState.CurrentState,
                InputValues = _inputState,
                Dt = Time.deltaTime,
            };

            UpdateInputState();

            // Buffer requested actions
            DispatchBufferedJump(_ruleState);

            //Debug stuff
            _debugContext.CurrentRequests = _requests;
            _debugContext.Facts = physicsContext;
            _debugContext.CurrentGameState = _gameState.CurrentState;
            _debugContext.RuleState = _ruleState;
            _debugContext.Dt = Time.deltaTime;

            // _debugSb.Clear();
            // string header = $"Requests for frame: {Time.frameCount}";
            // _debugSb.AppendLine(header);
            // for (int i = 0; i < _requests.Count; i++)
            // {
            //     _debugSb.AppendLine($"{_requests[i].RequestType} handled by {GetInstanceID()}");
            // }
            // if (_requests.Count > 0)
            // {
            //     Debug.Log(_debugSb);
            // }

            // Process this frame via core
            var actionResults = _appOrchestrator.ProcessActions(context);

            // Check for any actions that need to be handled immediately
            foreach (var result in actionResults)
            {
                if (result is IImmediateResult immediate)
                {
                    HandleImmeidateResult(immediate);
                }
            }


            // Build the effect queue
            _effectOrchestrator.BuildEffectQueue(actionResults);
            _effectOrchestrator.SetPhysicsContext(physicsContext);

            // Build the action queue
            _simulationDriver.EnqueueActionResults(actionResults);
            _simulationDriver.SetCurrentPhysicsState(physicsContext);

            // Animate the state
            _animatorBridge.SyncAnimation(_inputState, physicsContext, _ruleState);

            // Clear the state of this frame
            _requests.Clear();
        }

        private void HandleImmeidateResult(IImmediateResult result)
        {
            switch (result)
            {
                case PauseResult pause:
                    {
                        _changeGameState.ChangeGameState(GameState.Paused);
                        break;
                    }
            }
        }

        private void UpdateRequestList(IActionRequest newRequest)
        {
            _requests.Add(newRequest);
        }

        private void DispatchBufferedJump(PlayerRuleState _ruleState)
        {
            if (_ruleState.JumpBuffered)
            {
                // Debug.Log($"Buffered jump dispatched - frame {Time.frameCount}");
                _requests.Add(new JumpRequest(true));
            }
        }

        private void BufferInputs(IActionRequest request)
        {
            switch (request)
            {
                case JumpRequest jump:
                    {
                        _ruleState.StartJumpBufferTimer();
                        break;
                    }
            }
        }

        private void UpdateInputState()
        {
            // update inputs
            _inputState.LeftStick = _inputReader.LeftStick;
            _inputState.SprintPressed = _inputReader.SprintPressed;
            _inputState.JumpPressed = _inputReader.JumpPressed;
            _inputState.HoldingJump = _inputReader.HoldingJump;
            _inputState.GrabPressed = _inputReader.GrabPressed;
            _inputState.BarkPressed = _inputReader.BarkPressed;
            _inputState.BarkHeld = _inputReader.BarkHeld;
        }

        private IDispatchRequest[] BuildActionDispatchers()
        {
            var dispatchers = new IDispatchRequest[]
            {
                new JumpDispatcher(),
                new JumpCancelDispatcher(),
                new RunDispatcher(),
                new QuickStepDispatcher(),
                new BarkDispatcher(),
                new PauseDispatcher(),
            };

            return dispatchers;
        }


    }
}