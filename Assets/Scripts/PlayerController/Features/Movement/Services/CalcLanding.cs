using System;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Features.Movement.Abstractions;
using UnityEngine;

namespace PlayerController.Features.Movement.Services
{
    public class CalcLanding : ICalcAction
    {
        public PlayerStats Stats => _stats;
        private PlayerStats _stats;
        public CalcLanding(PlayerStats stats)
        {
            _stats = stats;
        }

        public KinematicResult Calculate(IActionResult actionResult, ref KinematicResult currentResult)
        {

            if (actionResult is not LandingResult landing || !actionResult.Approved) return currentResult;

            // Stop running
            // currentResult.Velocity.x = 0f;
            float target = 0f;


            float accelValue = _stats.BrakeAccel.Value;



            currentResult.Velocity.x = Mathf.MoveTowards(currentResult.Velocity.x, target, accelValue * currentResult.Dt);

            return currentResult;
        }
    }
}