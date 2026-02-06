using PlayerController.Core.State.DataStructures;
using System.Collections.Generic;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Effects.Abstractions;

namespace PlayerController.Application.Effects.DataStructures
{
    [System.Serializable]
    public class PlayerEffectContext
    {
        public List<IEffectRequest> CurrentRequests;
        public PlayerRuleState RuleState;
        public InputState InputValues;
    }
}