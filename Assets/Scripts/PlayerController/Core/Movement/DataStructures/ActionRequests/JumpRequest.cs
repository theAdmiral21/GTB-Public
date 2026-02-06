using System;

namespace PlayerController.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to request a jump from Core.
    /// </summary>
    public readonly struct JumpRequest : IActionRequest
    {
        public readonly Type RequestType => typeof(JumpRequest);
        public readonly bool Requested;
        public JumpRequest(bool requested)
        {
            Requested = requested;
        }

    }
}