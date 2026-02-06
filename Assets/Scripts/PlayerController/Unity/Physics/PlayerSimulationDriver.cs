using System.Collections.Generic;
using PlayerController.Application.Abstractions;
using PlayerController.Application.Movement.Abstractions;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Features.Movement;
using PlayerController.Unity.Movement;
using UnityEngine;

namespace PlayerController.Unity.Physics
{
    public class PlayerSimulationDriver : MonoBehaviour, IPlayerSimulationDriver
    {
        [SerializeField] private PhysicsMonitor _monitor;
        [SerializeField] private RaycastController _raycastController;
        [SerializeField] private MovementController _movementController;
        [SerializeField] private PlayerMovementSO _playerMovementSO;
        private ISolveKinematics _kinematicSolver;
        private IMovementResolver _movementResolver;
        public PlayerStats Stats => _playerStats;
        private PlayerStats _playerStats;
        private PhysicsContext _physicsContext;

        [Header("DEBUG – Kinematics")]
        [SerializeField] private KinematicResult _debugResult;
        private KinematicResult _currentState;

        private List<IActionResult> _queuedResults = new List<IActionResult>();
        private void Awake()
        {
            // Set up physics objects
            _playerStats = new PlayerStats(_playerMovementSO);

            _kinematicSolver = new KinematicSolver(_playerStats);
            _movementResolver = new MovementResolver(_raycastController, _movementController);

            _currentState = new KinematicResult
            {
                Velocity = Vector2.zero,
                Acceleration = Vector2.zero,
                Gravity = _playerStats.BaseGravity.Value,
                Dt = Time.fixedDeltaTime,
            };
        }
        public void EnqueueActionResults(List<IActionResult> actionResults)
        {
            _queuedResults.AddRange(actionResults);
        }

        public void SetCurrentPhysicsState(PhysicsContext context)
        {
            _physicsContext = context;
        }

        private void FixedUpdate()
        {
            // Get the buffered results
            var actionResults = _queuedResults;

            // Get dt
            float dt = Time.fixedDeltaTime;

            // Simulate the game
            AdvanceSimulation(dt, actionResults, _physicsContext);
            _queuedResults.Clear();

            // Update the physics monitor
            // Debug.Log($"Current state velocity sent to monitor: {_currentState.Velocity}");
            _monitor.ObserveVelocity(_currentState.Velocity);

            // Debugging
            _debugResult.Velocity = _currentState.Velocity;
            _debugResult.Acceleration = _currentState.Acceleration;
            _debugResult.Gravity = _currentState.Gravity;
        }

        public void AdvanceSimulation(float dt, List<IActionResult> actionResults, PhysicsContext physicsContext)
        {
            // Send the ActionResult list to the kinematic solver
            _currentState = _kinematicSolver.Solve(dt, actionResults, physicsContext, ref _currentState);

            Vector2 displacement = IntegrateVelocity(dt, ref _currentState);

            // Send that result to the movement resolver
            Vector2 _dispResolved = _movementResolver.ResolveMovement(displacement);

            // Finally send the movement vector to the movement controller
            _movementController.Move(_dispResolved);
        }

        private Vector2 IntegrateVelocity(float dt, ref KinematicResult state)
        {
            // Apply dt
            state.Velocity += (state.Acceleration + Vector2.up * state.Gravity) * dt;
            return state.Velocity * dt;
        }
    }
}