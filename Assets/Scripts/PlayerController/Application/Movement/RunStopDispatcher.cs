using PlayerController.Application.Abstractions;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using Primitives.Common.State.Enums;
using UnityEngine;

namespace PlayerController.Application.Movement.Services
{
    public class RunStopDispatcher : DispatchRequestBase<RunStopRequest, RunStopResult>
    {

        protected override RunStopResult Dispatch(in RunStopRequest request, in PhysicsContext facts, in GameState gameState, in InputState inputs, ref PlayerRuleState ruleState)
        {
            return RunStopRules.TryStopRun(inputs, facts, ref ruleState);
        }
    }
}