using PlayerController.Application.Movement.DataStructures;
using UnityEngine;

namespace PlayerController.Application.Abstractions
{
    public interface IMovementResolver
    {
        public Vector2 ResolveMovement(Vector2 velocity, bool isOnPlatform = false);
    }
}