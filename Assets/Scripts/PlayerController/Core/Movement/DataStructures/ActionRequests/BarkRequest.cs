using System;
using PlasticGui.WorkspaceWindow.PendingChanges;
using PlayerController.Core.Effects.Abstractions;

namespace PlayerController.Core.Movement.DataStructures
{
    public struct BarkRequest : IActionRequest
    {
        public bool Requested => _requested;
        private bool _requested;

        public Type RequestType => typeof(BarkRequest);

        public BarkRequest(bool requested)
        {
            _requested = requested;
        }
    }
}