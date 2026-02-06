using System.Collections.Generic;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.State.DataStructures;

namespace PlayerController.Application.Movement.DataStructures
{
    public readonly struct ProcessedActions
    {
        public readonly List<IActionResult> ActionResults;
        public readonly PlayerRuleState RuleState;

        public ProcessedActions(List<IActionResult> actionResults, PlayerRuleState ruleState)
        {
            ActionResults = actionResults;
            RuleState = ruleState;
        }
    }
}