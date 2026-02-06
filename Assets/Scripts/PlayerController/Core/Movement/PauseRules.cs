using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Inputs.DataStructures;
using Primitives.Common.State.Enums;
using UnityEngine;

namespace PlayerController.Core.Movement
{
    /// <summary>
    /// This class is used to evaluate the following rules for pausing:
    /// - Can only pause while in game
    /// </summary>
    public static class PauseRules
    {
        public static PauseResult TryPause(PauseRequest request, PhysicsContext facts, GameState gameState, InputState inputValues, ref PlayerRuleState ruleState)
        {
            if (gameState == GameState.Gameplay)
            {
                return Approved();
            }

            return Denied();
        }

        private static PauseResult Approved()
        {
            Debug.Log("pause approved");
            return new PauseResult(true, ActionPhase.Override);
        }
        private static PauseResult Denied()
        {
            Debug.Log("pause denied");
            return new PauseResult(false, ActionPhase.Override);

        }
    }
}