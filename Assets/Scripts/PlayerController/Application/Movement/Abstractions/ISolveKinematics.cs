using System.Collections.Generic;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Stats;

namespace PlayerController.Application.Movement.Abstractions
{
    public interface ISolveKinematics
    {
        public PlayerStats Stats { get; }
        public KinematicResult Solve(float dt, List<IActionResult> results, PhysicsContext physicsContext, ref KinematicResult _currentState);
    }
}