using System;

namespace PlayerController.Core.Movement.Abstractions
{
    public interface IActionResult
    {
        public Type ResultType { get; }
        public bool Approved { get; }
        public ActionPhase Phase { get; }
    };
}