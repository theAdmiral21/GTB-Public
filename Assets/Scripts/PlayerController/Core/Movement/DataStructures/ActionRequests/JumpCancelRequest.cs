using System;

namespace PlayerController.Core.Movement.DataStructures
{
    /// <summary>
    /// Public struct sent from application layer to cancel a jump from Core.
    /// </summary>
    public struct JumpCancelRequest : IActionRequest
    {
        public bool Requested;

        public Type RequestType => typeof(JumpCancelRequest);
    }
}