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
    public static class QuickStepRules
    {
        public static QuickStepResult TryQuickStep(QuickStepRequest request, PhysicsContext facts, InputState inputs, ref PlayerRuleState ruleState)
        {
            // Basic checks
            if (!request.Requested || ruleState.IsDisabled) return Denied();

            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                ruleState.StartBlockXTimer();
                return Approved(request.Direction);
            }

            return Denied();
        }

        private static QuickStepResult Approved(float direction)
        {
            // Debug.Log("QuickStep approved");
            return new QuickStepResult(true, direction, ActionPhase.Impulse);
        }
        private static QuickStepResult Denied()
        {
            // Debug.Log("QuickStep denied");
            return new QuickStepResult(false, 1, ActionPhase.Impulse);

        }
    }
}