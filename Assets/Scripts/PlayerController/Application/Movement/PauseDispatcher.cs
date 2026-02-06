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
    public sealed class PauseDispatcher : DispatchRequestBase<PauseRequest, PauseResult>
    {
        protected override PauseResult Dispatch(in PauseRequest request, in PhysicsContext facts, in GameState gameState, in InputState inputs, ref PlayerRuleState ruleState)
        {
            return PauseRules.TryPause(request, facts, gameState, inputs, ref ruleState);
        }
    }
}