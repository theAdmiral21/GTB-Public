using System;
using PlayerController.Core.Movement.Abstractions;

namespace PlayerController.Core.Movement.DataStructures
{
    public struct FallResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public FallType Type;

        public Type ResultType => typeof(FallResult);

        public FallResult(bool approved, FallType type, ActionPhase phase)
        {
            _approved = approved;
            Type = type;
            _phase = phase;
        }
    }
}