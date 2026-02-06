using System;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using Primitives.Common.State.Enums;

namespace PlayerController.Application.Abstractions
{
    public interface IDispatchRequest
    {
        public Type DispatchType { get; }

        IActionResult DispatchUntyped(
            IActionRequest request,
            in PhysicsContext facts,
            in GameState gameState,
            in InputState inputs,
            ref PlayerRuleState ruleState);
    }
}