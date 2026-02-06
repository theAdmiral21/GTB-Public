using PlayerController.Application.Abstractions;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using Primitives.Common.State.Enums;

namespace PlayerController.Application.Movement.Services
{
    public sealed class JumpDispatcher : DispatchRequestBase<JumpRequest, JumpResult>
    {
        protected override JumpResult Dispatch(in JumpRequest request, in PhysicsContext facts, in GameState gameState, in InputState inputs, ref PlayerRuleState ruleState)
        {
            return JumpRules.TryJump(request, facts, inputs, ref ruleState);
        }
    }
}