using System;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;

namespace PlayerController.Application.Abstractions
{
    public abstract class DispatchEffectBase<TRequest, TResult> : IDispatchEffect
    where TRequest : struct, IEffectRequest
    where TResult : struct, IEffectResult
    {
        public Type EffectType => typeof(TRequest);

        public IEffectResult DispatchUntyped(
            IEffectRequest request,
            in InputState inputs,
            ref PlayerRuleState ruleState)
        {
            return Dispatch(
            (TRequest)request,
            inputs,
            ref ruleState);
        }

        protected abstract TResult Dispatch(
            in TRequest request,
            in InputState inputs,
            ref PlayerRuleState ruleState);
    }
}