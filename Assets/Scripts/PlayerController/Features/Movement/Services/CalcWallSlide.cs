using PlayerController.Application.Abstractions;
using PlayerController.Application.Inputs.DataStructures;
using PlayerController.Application.Physics.DataStructures;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Stats;
using Primitives.Common.State.Enums;
using UnityEngine;

namespace PlayerController.Features.Movement.Services
{
    public class WallSlideDispatcher : DispatchRequestBase<WallSlideRequest, WallSlideResult>
    {
        public PlayerStats Stats => _stats;
        private PlayerStats _stats;

        public WallSlideDispatcher(PlayerStats stats)
        {
            _stats = stats;
        }

        protected override WallSlideResult Dispatch(in WallSlideRequest request, in PhysicsContext facts, in GameState gameState, in InputState inputs, ref PlayerRuleState ruleState)
        {
            return new WallSlideResult(false, ActionPhase.Continuous);
        }
    }
}