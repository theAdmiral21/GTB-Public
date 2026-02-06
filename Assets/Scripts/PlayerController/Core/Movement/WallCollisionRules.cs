using System.ComponentModel.Design;
using UnityEngine;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Physics.DataStructures;

namespace PlayerController.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for running:
    /// - Running into walls halts movement.
    /// 
    /// </summary>
    public static class WallCollisionRules
    {
        public static RunStopResult OnContact(PhysicsContext facts, ref PlayerRuleState ruleState)
        {
            if (facts.WallContactType == WallContact.None) return Denied();
            if (ruleState.XInputLocked) return Denied();
            // Basic checks
            if (ruleState.Dir == 1 && facts.WallContactType == WallContact.Right)
                return Approved(RunType.Run);

            if (ruleState.Dir == -1 && facts.WallContactType == WallContact.Left)
                return Approved(RunType.Run);

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