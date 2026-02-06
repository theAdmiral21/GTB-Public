using PlayerController.Core.State.DataStructures;
using System.Collections.Generic;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.Inputs.DataStructures;
using Primitives.Common.State.Enums;

namespace PlayerController.Application.Movement.DataStructures
{
    [System.Serializable]
    public class PlayerActionContext
    {
        public List<IActionRequest> CurrentRequests;
        public PhysicsContext Facts;
        public PlayerRuleState RuleState;
        public GameState CurrentGameState;
        public InputState InputValues;
        public float Dt;
    }
}