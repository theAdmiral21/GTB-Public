using PlayerController.Application.Abstractions;
using PlayerController.Core.Effects;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using Primitives.Common.State.Enums;

namespace PlayerController.Application.Movement.Services
{
    public sealed class BarkDispatcher : DispatchRequestBase<BarkRequest, BarkResult>
    {
        protected override BarkResult Dispatch(in BarkRequest request, in PhysicsContext facts, in GameState gameState, in InputState inputs, ref PlayerRuleState ruleState)
        {
            return BarkRules.TryBark(request, inputs, ruleState);
        }
    }
}