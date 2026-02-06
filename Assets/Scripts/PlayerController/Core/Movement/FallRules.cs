using UnityEngine;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;

namespace PlayerController.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for falling:
    /// - The player is not grounded
    /// - The player is touching a wall and is not grounded
    /// - The player jumped is falling -> slow fall
    /// - If the player is wall jumping, do not override gravity
    /// 
    /// </summary>
    public static class FallRules
    {
        public static FallResult TryFall(PhysicsContext facts, ref PlayerRuleState ruleState)
        {

            // Do not override gravity during a wall jump
            if (ruleState.XInputLocked) return Denied();

            if (!facts.IsGrounded && !facts.IsOnPlatform && !facts.IsRising && !facts.IsWallSliding)
            {
                ruleState.ApplyGravity = true;
                ruleState.FallType = FallType.Slow;
                return Approved(FallType.Slow);
            }


            if (facts.IsWallSliding)
            {
                ruleState.ApplyGravity = false;
                ruleState.FallType = FallType.WallSlide;
                return Approved(FallType.WallSlide);
            }

            // NOTE Need a way to check input states here for slow and fast fall

            ruleState.ApplyGravity = false;
            ruleState.FallType = FallType.None;
            return Denied();

        }

        private static FallResult Approved(FallType type)
        {
            // Debug.Log($"Fall approved, type: {type}");
            return new FallResult
            (
                true,
                type,
                ActionPhase.Continuous
            );
        }
        private static FallResult Denied()
        {
            // Debug.Log("Fall denied");
            return new FallResult
            (
                false,
                FallType.None,
                ActionPhase.Continuous
            );
        }

    }
}