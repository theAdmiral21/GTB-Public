using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Features.Movement.Abstractions;
using UnityEngine;

namespace PlayerController.Features.Movement.Services
{
    public class CalcJumpCancel : ICalcAction
    {
        public PlayerStats Stats => _stats;
        private PlayerStats _stats;
        public CalcJumpCancel(PlayerStats stats) => _stats = stats;

        public KinematicResult Calculate(IActionResult actionResult, ref KinematicResult currentResult)
        {
            if (actionResult is not JumpCancelResult jumpCancel) return currentResult;

            if (!jumpCancel.Approved) return currentResult;

            // Calculate the jump cancel variables
            currentResult.Gravity = Stats.BaseGravity.Value * Stats.SlowFall.Value;
            currentResult.Velocity.y = 0f;
            return currentResult;
        }

    }
}