using System;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using Primitives.Common.State.Enums;

namespace PlayerController.Application.Abstractions
{
    public abstract class DispatchRequestBase<TRequest, TResult> : IDispatchRequest
    where TRequest : struct, IActionRequest
    where TResult : struct, IActionResult
    {
        public Type DispatchType => typeof(TRequest);

        public IActionResult DispatchUntyped(
            IActionRequest request,
            in PhysicsContext facts,
            in GameState gameState,
            in InputState inputs,
            ref PlayerRuleState ruleState)
        {
            return Dispatch(
            (TRequest)request,
            facts,
            gameState,
            inputs,
            ref ruleState);
        }

        protected abstract TResult Dispatch(
            in TRequest request,
            in PhysicsContext facts,
            in GameState gameState,
            in InputState inputs,
            ref PlayerRuleState ruleState);
    }
}