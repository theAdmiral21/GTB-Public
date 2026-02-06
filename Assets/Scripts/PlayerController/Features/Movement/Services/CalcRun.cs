using System;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Features.Movement.Abstractions;
using UnityEngine;

namespace PlayerController.Features.Movement.Services
{
    public class CalcRun : ICalcAction
    {
        public PlayerStats Stats => _stats;
        private PlayerStats _stats;
        private float _xSmoothing;
        public CalcRun(PlayerStats stats)
        {
            _stats = stats;
        }

        public KinematicResult Calculate(IActionResult actionResult, ref KinematicResult currentResult)
        {

            if (actionResult is not RunResult run || !actionResult.Approved) return currentResult;

            float target = 0;
            float accelValue = 0;
            float dir = run.Value.x >= 0 ? 1f : -1f;
            float vDir = currentResult.Velocity.x >= 0 ? 1f : -1f;

            float xInput = run.Value.x;
            if (run.Type == RunType.Run)
            {
                // Calculate the run speed
                target = _stats.RunSpeed.Value * xInput;
                if (dir == vDir)
                {
                    accelValue = _stats.RunAccel.Value;
                }
                else
                {
                    accelValue = _stats.BrakeAccel.Value;
                }
            }
            else if (run.Type == RunType.Aerial)
            {
                // Calculate Aerial movement speed
                target = _stats.RunSpeed.Value * xInput;
                if (dir == vDir)
                {
                    accelValue = _stats.AerialAccel.Value;
                }
                else
                {
                    accelValue = _stats.AerialBrake.Value;
                }
            }
            else if (run.Type == RunType.Sprint)
            {
                // Gotta go fast!
                target = _stats.SprintSpeed.Value * xInput;

                if (dir == vDir)
                {
                    accelValue = _stats.SprintAccel.Value;
                }
                else
                {
                    accelValue = _stats.BrakeAccel.Value;
                }
            }

            // Smooth things out
            // Debug.Log($"accel value: {accelValue}");
            currentResult.Velocity.x = Mathf.MoveTowards(currentResult.Velocity.x, target, accelValue * currentResult.Dt);
            // currentResult.Velocity.x = target;

            return currentResult;
        }
    }
}