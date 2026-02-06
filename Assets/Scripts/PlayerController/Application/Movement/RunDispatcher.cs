using PlayerController.Application.Abstractions;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using Primitives.Common.State.Enums;
using UnityEngine;

namespace PlayerController.Application.Movement.Services
{
    public class RunDispatcher : DispatchRequestBase<RunRequest, RunResult>
    {

        protected override RunResult Dispatch(in RunRequest request, in PhysicsContext facts, in GameState gameState, in InputState inputs, ref PlayerRuleState ruleState)
        {
            // Debug.Log("Dispatching run");
            return RunRules.TryRun(request, facts, inputs, ref ruleState);

        }
    }
}