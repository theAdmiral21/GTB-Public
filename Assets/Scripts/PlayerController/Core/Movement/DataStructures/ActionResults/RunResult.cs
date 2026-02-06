using System;
using UnityEngine;
using PlayerController.Core.Movement.Abstractions;

namespace PlayerController.Core.Movement.DataStructures
{
    public struct RunResult : IActionResult
    {
        public bool Approved => _approved;
        private readonly bool _approved;
        public ActionPhase Phase => _phase;
        private readonly ActionPhase _phase;
        public RunType Type;
        public readonly Vector2 Value;

        public Type ResultType => typeof(RunResult);
        public RunResult(bool approved, Vector2 value, RunType type, ActionPhase phase)
        {
            _approved = approved;
            Value = value;
            Type = type;
            _phase = phase;
        }
    }
}