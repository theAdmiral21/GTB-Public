using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Features.Movement.Abstractions;
using UnityEngine;

namespace PlayerController.Features.Movement.Services
{
    public class CalcFall : ICalcAction
    {
        public PlayerStats Stats => _stats;
        private PlayerStats _stats;
        public CalcFall(PlayerStats stats) => _stats = stats;

        public KinematicResult Calculate(IActionResult actionResult, ref KinematicResult currentResult)
        {
            if (actionResult is not FallResult fall) return currentResult;

            // If we can't fall then don't
            if (!fall.Approved)
            {
                return currentResult;
            }
            else
            {
                if (fall.Type == FallType.Fast)
                {
                    currentResult.Gravity = Stats.BaseGravity.Value * Stats.FastFall.Value;
                }
                else if (fall.Type == FallType.Slow)
                {
                    currentResult.Gravity = Stats.BaseGravity.Value * Stats.SlowFall.Value;
                }
                else if (fall.Type == FallType.WallSlide)
                {
                    currentResult.Velocity.y = Stats.WallSlideSpeed.Value;
                    currentResult.Gravity = 0;
                }
            }

            return currentResult;
        }

    }
}