using UnityEngine;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Inputs.DataStructures;
using Primitives.Audio;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for when the player lands on a surface:
    /// - If there is no movement input, the player will stop.
    /// - If the surface they landed on is slippery, momentum will be conserved.
    /// 
    /// </summary>
    public static class LandingRules
    {
        public static LandingResult OnLand(PhysicsContext facts, ref PlayerRuleState ruleState, ref InputState inputs)
        {
            // if (!ruleState.GroundedLastFrame && facts.IsGrounded && ruleState.LandingStopRequested && inputs.LeftStick.x == 0)
            if (!ruleState.GroundedLastFrame && facts.IsGrounded)
            {
                // Debug.Log($"Landed on surface type: {facts.Surface}");
                ruleState.LandingStopRequested = false;
                return Approved(facts);
            }
            return Denied();
        }

        private static LandingResult Approved(PhysicsContext physicsContext)
        {
            // Debug.Log("Run stop approved");
            return new LandingResult(true, physicsContext.Surface, ActionPhase.Impulse);
        }
        private static LandingResult Denied()
        {
            // Debug.Log("Run stop denied");
            return new LandingResult(false, SurfaceType.None, ActionPhase.Impulse);

        }
    }
}