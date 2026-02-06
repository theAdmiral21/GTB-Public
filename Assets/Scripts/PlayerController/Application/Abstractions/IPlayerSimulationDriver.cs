using System.Collections.Generic;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.State.DataStructures;

namespace PlayerController.Application.Abstractions
{
    public interface IPlayerSimulationDriver
    {
        public void EnqueueActionResults(List<IActionResult> actionResults);
        public void SetCurrentPhysicsState(PhysicsContext context);
        public void AdvanceSimulation(float dt, List<IActionResult> actionResults, PhysicsContext physicsContext);
    }
}