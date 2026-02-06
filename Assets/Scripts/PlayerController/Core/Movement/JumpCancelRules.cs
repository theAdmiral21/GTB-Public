using UnityEngine;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;

namespace PlayerController.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for a jump cancel:
    /// - The player must be in the air
    /// - The player must not be holding the jump button
    /// - The player must not be wall sliding
    /// - The player can not jump cancel during a wall jump
    /// 
    /// </summary>
    public static class JumpCancelRules
    {
        public static JumpCancelResult TryJumpCancel(JumpCancelRequest request, PhysicsContext facts, ref PlayerRuleState ruleState)
        {
            if (!request.Requested) return Denied();

            // You must wait for the wall jump to complete before jumping
            if (ruleState.XInputLocked) return Denied();

            if (!facts.IsGrounded && !facts.IsOnPlatform && !facts.IsWallSliding && facts.IsRising)
            {
                ruleState.ApplyGravity = true;
                return Approved();
            }
            return Denied();
        }

        private static JumpCancelResult Approved()
        {
            // Debug.Log("Jump cancel approved");
            return new JumpCancelResult
            (
                true,
                ActionPhase.Impulse
            );
        }
        private static JumpCancelResult Denied()
        {
            // Debug.Log("Jump cancel denied");
            return new JumpCancelResult
            (
                 false,
                 ActionPhase.Impulse
            );
        }
    }
}