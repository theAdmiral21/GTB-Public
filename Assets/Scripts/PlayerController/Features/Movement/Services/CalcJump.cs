using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Features.Movement.Abstractions;
using UnityEngine;

namespace PlayerController.Features.Movement.Services
{
    public class CalcJump : ICalcAction
    {
        public PlayerStats Stats => _stats;
        private PlayerStats _stats;
        public CalcJump(PlayerStats stats) => _stats = stats;

        public KinematicResult Calculate(IActionResult actionResult, ref KinematicResult currentResult)
        {
            if (actionResult is not JumpResult jump || !actionResult.Approved) return currentResult;

            if (jump.Type != JumpType.WallJump)
            {
                // Calculate the jump variables
                float gravity = -2 * _stats.JumpHeight.Value / Mathf.Pow(_stats.JumpApexTime.Value, 2);
                currentResult.Gravity = gravity;
                currentResult.Velocity.y = Mathf.Abs(gravity) * _stats.JumpApexTime.Value;
            }
            else
            {
                float gravity = -2 * _stats.WallJumpHeight.Value / Mathf.Pow(_stats.WallJumpApexTime.Value, 2);
                currentResult.Gravity = gravity;
                currentResult.Velocity.y = Mathf.Abs(gravity) * _stats.WallJumpApexTime.Value;
                currentResult.Velocity.x = -jump.Dir * _stats.WallJumpVelocity.Value;
                // Debug.Log($"Wall jump velocity: {currentResult.Velocity}");
            }

            return currentResult;
        }

    }
}