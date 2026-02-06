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
    public sealed class JumpCancelDispatcher : DispatchRequestBase<JumpCancelRequest, JumpCancelResult>
    {
        protected override JumpCancelResult Dispatch(in JumpCancelRequest request, in PhysicsContext facts, in GameState gameState, in InputState inputs, ref PlayerRuleState ruleState)
        {
            return JumpCancelRules.TryJumpCancel(request, facts, ref ruleState);
        }
    }
}