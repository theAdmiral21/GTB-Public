using UnityEngine;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Inputs.DataStructures;

namespace PlayerController.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for a jump:
    /// - The player can only jump if they're grounded.
    /// - The player can only coyote jump if they were previously grounded.
    /// - The player can only double jump if they have an available jump.
    /// - A jump actions costs 1 jump
    /// 
    /// Coyote jump rules:
    /// - the player must NOT be grounded
    /// - the coyote jump timer must be > 0
    /// - the coyote jump timer starts when:
    ///     - the player was grounded last frame and is not grounded this frame
    ///     - the player's y velocity is < 0
    /// 
    /// </summary>
    public static class JumpRules
    {
        public static JumpResult TryJump(JumpRequest request, PhysicsContext facts, InputState inputValues, ref PlayerRuleState ruleState)
        {
            if (!request.Requested || ruleState.IsDisabled) return Denied(ruleState.Dir);

            if (facts.IsGrounded || facts.IsOnPlatform)
            {
                // Reset the buffer timer
                ruleState.ResetBufferTimer();
                // Eat a jump
                ruleState.RemainingJumps -= 1;
                return Approved(JumpType.Ground, 1, ruleState.Dir);
            }

            // Coyote Jump
            if (ruleState.CanCoyoteJump)
            {
                // Reset the buffer timer
                ruleState.ResetBufferTimer();
                // Eat a jump
                ruleState.RemainingJumps -= 1;
                return Approved(JumpType.Coyote, 1, ruleState.Dir);
            }

            if (facts.IsWallSliding)
            {
                // Are wall jumps free? I think they are because you jump onto the wall and that eats a jump, but when you wall jump, you should be able to double jump after. So wall jumps are free

                // Reset the buffer timer
                ruleState.ResetBufferTimer();
                // Debug.Log("Called block x timer");
                ruleState.StartBlockXTimer();
                return Approved(JumpType.WallJump, 0, ruleState.Dir);
            }

            // Double Jump
            if (ruleState.RemainingJumps > 0 && ruleState.UngroundedCounter > .1f && !ruleState.CanCoyoteJump)
            {
                // Reset the buffer timer
                ruleState.ResetBufferTimer();
                // Eat a jump
                ruleState.RemainingJumps -= 1;
                return Approved(JumpType.Double, 1, ruleState.Dir);
            }

            return Denied(ruleState.Dir);
        }

        private static JumpResult Approved(JumpType type, int consumed, float dir)
        {
            // Debug.Log($"{type} approved");
            return new JumpResult
            (
                true,
                type,
                consumed,
                dir,
                ActionPhase.Impulse
            );
        }
        private static JumpResult Denied(float dir)
        {
            // Debug.Log($"Jump denied");
            return new JumpResult
            (
                false,
                JumpType.None,
                0,
                dir,
                ActionPhase.Impulse
            );
        }
    }
}