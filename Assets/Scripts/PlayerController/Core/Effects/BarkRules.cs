using UnityEngine;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement;
using PlayerController.Core.Movement.DataStructures;

namespace PlayerController.Core.Effects
{
    public static class BarkRules
    {
        public static BarkResult TryBark(BarkRequest request, InputState inputs, PlayerRuleState ruleState)
        {
            // Debug.Log("Got bark!");
            if (!request.Requested || ruleState.IsDisabled) return Denied();
            // Does anything need to go in here?
            return Approved();
        }

        private static BarkResult Approved()
        {
            return new BarkResult(true, ActionPhase.Impulse);
        }

        private static BarkResult Denied()
        {
            return new BarkResult(false, ActionPhase.Impulse);
        }
    }
}