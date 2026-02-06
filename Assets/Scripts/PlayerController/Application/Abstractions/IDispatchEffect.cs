using System;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;

namespace PlayerController.Application.Abstractions
{
    public interface IDispatchEffect
    {
        public Type EffectType { get; }

        IEffectResult DispatchUntyped(
            IEffectRequest request,
            in InputState inputs,
            ref PlayerRuleState ruleState);
    }
}