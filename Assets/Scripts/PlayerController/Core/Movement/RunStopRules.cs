using System.ComponentModel.Design;
using UnityEngine;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Inputs.DataStructures;

namespace PlayerController.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for stopping a run:
    /// - The player can stop only when grounded
    /// 
    /// </summary>
    public static class RunStopRules
    {
        public static RunStopResult TryStopRun(InputState inputs, PhysicsContext facts, ref PlayerRuleState ruleState)
        {

            if (Mathf.Abs(inputs.LeftStick.x) < 0.25f)
                if (facts.IsGrounded || facts.IsOnPlatform)
                {
                    return Approved(RunType.Run);
                }
                else
                {
                    ruleState.LandingStopRequested = true;
                    return Approved(RunType.Aerial);
                }

            return Denied();
        }


        private static RunStopResult Approved(RunType type)
        {
            // Debug.Log("Run stop approved");
            return new RunStopResult(true, type, ActionPhase.Continuous);
        }
        private static RunStopResult Denied()
        {
            // Debug.Log("Run stop denied");
            return new RunStopResult(false, RunType.None, ActionPhase.Continuous);

        }
    }
}