using System.Collections.Generic;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement;
using System.Linq;
using PlayerController.Application.Movement;

namespace PlayerController.Application
{
    /// <summary>
    /// Orchestrator that recieves Action Requests and evaluates them using Core rules.
    /// </summary>
    public class AppOrchestrator
    {
        private ActionDispatcher _actionDispatcher;

        private List<IActionResult> _actionResults = new List<IActionResult>();


        public AppOrchestrator(
            ActionDispatcher actionDispatcher)
        {
            _actionDispatcher = actionDispatcher;
        }
        public List<IActionResult> ProcessActions(PlayerActionContext actionContext)
        {
            // Update core rules
            actionContext.RuleState.UpdateRules(actionContext.InputValues, actionContext.Facts, actionContext.Dt);

            // Evaluate requested actions
            _actionResults = _actionDispatcher.DispatchActions(actionContext);

            // Evaluate external/environmental effects
            var fallResult = FallRules.TryFall(actionContext.Facts, ref actionContext.RuleState);
            _actionResults.Add(fallResult);

            var landingStopResult = LandingRules.OnLand(actionContext.Facts, ref actionContext.RuleState, ref actionContext.InputValues);
            _actionResults.Add(landingStopResult);

            var wallStopResult = WallCollisionRules.OnContact(actionContext.Facts, ref actionContext.RuleState);
            _actionResults.Add(wallStopResult);

            var runStopResult = RunStopRules.TryStopRun(actionContext.InputValues, actionContext.Facts, ref actionContext.RuleState);
            _actionResults.Add(runStopResult);

            // After evaluation set values for the previous frame 
            actionContext.RuleState.GroundedLastFrame = actionContext.Facts.IsGrounded;


            // sort the action results by phase priority. Impulse happens before continous actions. ie Jump is calculated before Run.
            var orderedResults = _actionResults.OrderBy(r => r.Phase).ToList();

            return orderedResults;
        }
    }
}