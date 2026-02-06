using System.Collections;
using System.Collections.Generic;
using Codice.CM.SEIDInfo;
using PlayerController.Application.Movement.Abstractions;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Features.Movement.Abstractions;
using PlayerController.Features.Movement.Services;
using UnityEngine;

namespace PlayerController.Features.Movement
{
    public class KinematicSolver : ISolveKinematics
    {
        public PlayerStats Stats => _stats;
        private PlayerStats _stats;

        private ICalcAction _calcJump;
        private ICalcAction _calcFall;
        private ICalcAction _calcRun;
        private ICalcAction _calcRunStop;
        private ICalcAction _calcLanding;
        private ICalcAction _calcJumpCancel;
        private ICalcAction _calcQuickStep;

        public KinematicSolver(PlayerStats stats)
        {
            _stats = stats;

            // Set up your calculation actions
            _calcJump = new CalcJump(_stats);
            _calcFall = new CalcFall(_stats);
            _calcRun = new CalcRun(_stats);
            _calcRunStop = new CalcRunStop(_stats);
            _calcLanding = new CalcLanding(_stats);
            _calcJumpCancel = new CalcJumpCancel(_stats);
            _calcQuickStep = new CalcQuickStep(stats);
        }

        public KinematicResult Solve(float dt, List<IActionResult> results, PhysicsContext physicsContext, ref KinematicResult _currentState)
        {
            _currentState.Dt = dt;
            // Stop increasing fall speed when grounded.
            if (physicsContext.IsGrounded && !physicsContext.IsOnPlatform && _currentState.Velocity.y < 0)
            {
                _currentState.Velocity.y = 0;
                _currentState.Gravity = 0;
            }

            foreach (IActionResult result in results)
            {
                if (!result.Approved) continue;

                switch (result)
                {
                    case JumpResult jump:
                        {
                            // Debug.Log("Jump case");
                            _currentState = _calcJump.Calculate(jump, ref _currentState);
                            break;
                        }
                    case JumpCancelResult jumpCancel:
                        {
                            // Debug.Log("Jump cancel case");
                            _currentState = _calcJumpCancel.Calculate(jumpCancel, ref _currentState);
                            break;
                        }
                    case FallResult fall:
                        {
                            // Debug.Log("Fall case");
                            _currentState = _calcFall.Calculate(fall, ref _currentState);
                            break;
                        }
                    case RunResult run:
                        {
                            // Debug.Log("Run case");
                            _currentState = _calcRun.Calculate(run, ref _currentState);
                            break;
                        }
                    case RunStopResult runStop:
                        {
                            // Debug.Log("Run stop case");
                            _currentState = _calcRunStop.Calculate(runStop, ref _currentState);
                            break;
                        }
                    case LandingResult landing:
                        {
                            // Debug.Log($"Landing case");
                            _currentState = _calcLanding.Calculate(landing, ref _currentState);
                            break;
                        }
                    case QuickStepResult quickStep:
                        {
                            // Debug.Log("Quick step case");
                            _currentState = _calcQuickStep.Calculate(quickStep, ref _currentState);
                            break;
                        }
                }
                // Debug.Log($"Returning new kinematic state from action {result.ResultType}: vel: {_currentState.Velocity}, accel: {_currentState.Acceleration}, gravity: {_currentState.Gravity}");
            }

            return _currentState;
        }
    }
}