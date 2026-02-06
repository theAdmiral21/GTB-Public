using PlayerController.Application.Abstractions;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using Primitives.Common.State.Enums;
using UnityEngine;

namespace PlayerController.Application.Movement.Services
{
    public class QuickStepDispatcher : DispatchRequestBase<QuickStepRequest, QuickStepResult>
    {

        protected override QuickStepResult Dispatch(in QuickStepRequest request, in PhysicsContext facts, in GameState gameState, in InputState inputs, ref PlayerRuleState ruleState)
        {
            // Debug.Log("Dispatching quick step");
            return QuickStepRules.TryQuickStep(request, facts, inputs, ref ruleState);

        }
    }
}