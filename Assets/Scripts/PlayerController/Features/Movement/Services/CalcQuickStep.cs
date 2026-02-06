using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Features.Movement.Abstractions;
using UnityEngine;

namespace PlayerController.Features.Movement.Services
{
    public class CalcQuickStep : ICalcAction
    {
        public PlayerStats Stats => _stats;
        private PlayerStats _stats;
        public CalcQuickStep(PlayerStats stats) => _stats = stats;

        public KinematicResult Calculate(IActionResult actionResult, ref KinematicResult currentResult)
        {
            if (actionResult is not QuickStepResult quickStep) return currentResult;

            if (!quickStep.Approved) return currentResult;

            // The velocity for a quick step action is determined by the distance the player can travel in one frame delta. This should be a very quick action over a short distance.
            currentResult.Velocity.x = _stats.QuickStepDistance.Value * quickStep.Direction;
            // Debug.Log($"QuickStep velocity: {currentResult.Velocity}");
            return currentResult;
        }

    }
}