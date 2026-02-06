using System;


namespace PlayerController.Core.Movement.Abstractions
{
    public struct PauseResult : IImmediateResult
    {
        public bool Approved => _approved;
        private bool _approved;

        public Type ResultType => typeof(PauseResult);

        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;

        public PauseResult(bool approved, ActionPhase phase)
        {
            _approved = approved;
            _phase = phase;
        }
    }
}