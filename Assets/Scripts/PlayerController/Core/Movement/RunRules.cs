using System.ComponentModel.Design;
using UnityEngine;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Physics.DataStructures;
using PlayerController.Core.Inputs.DataStructures;

namespace PlayerController.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for running:
    /// - The player can only run if the x input is not locked.
    /// - Running into walls halts movement.
    /// 
    /// </summary>
    public static class RunRules
    {
        public static RunResult TryRun(RunRequest request, PhysicsContext facts, InputState inputs, ref PlayerRuleState ruleState)
        {
            // Basic checks
            if (!request.Requested || ruleState.IsDisabled) return Denied();

            // If the player is wall jumping, block x input
            if (ruleState.XInputLocked) return Denied();

            SetDirection(request, ruleState);

            if (ruleState.Dir == 1 && facts.WallContactType == WallContact.Right)
                return Denied();

            if (ruleState.Dir == -1 && facts.WallContactType == WallContact.Left)
                return Denied();

            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                ruleState.LandingStopRequested = false;
                if (inputs.SprintPressed)
                {
                    return Approved(RunType.Sprint, request.Value);
                }
                return Approved(RunType.Run, request.Value);
            }

            if (!facts.IsGrounded || !facts.IsOnPlatform)
            {
                ruleState.LandingStopRequested = false;
                if (inputs.SprintPressed)
                {
                    return Approved(RunType.Sprint, request.Value);
                }
                return Approved(RunType.Aerial, request.Value);
            }

            return Denied();
        }

        private static void SetDirection(RunRequest request, PlayerRuleState ruleState)
        {
            var reqDir = Mathf.Sign(request.Value.x);
            if (reqDir == 1)
            {
                ruleState.Dir = 1;
            }
            else if (reqDir == -1)
            {
                ruleState.Dir = -1;
            }
        }
        private static RunResult Approved(RunType type, Vector2 value)
        {
            // Debug.Log("Run approved");
            return new RunResult(true, value, type, ActionPhase.Continuous);
        }
        private static RunResult Denied()
        {
            // Debug.Log("Run denied");
            return new RunResult(false, Vector2.zero, RunType.None, ActionPhase.Continuous);

        }
    }
}