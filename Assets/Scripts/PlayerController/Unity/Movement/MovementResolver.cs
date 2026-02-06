using PlayerController.Application.Abstractions;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Features.Abstractions;
using UnityEngine;

namespace PlayerController.Unity.Movement
{
    public class MovementResolver : IMovementResolver
    {
        private readonly IRaycastController _rayCaster;
        private Vector2 _velocity;
        public MovementResolver(IRaycastController raycastController, IMovePlayer movementController)
        {
            _rayCaster = raycastController;
        }

        public Vector2 ResolveMovement(Vector2 velocity, bool isOnPlatform = false)
        {
            _rayCaster.UpdateRaycastOrigins();
            // Clear all collisions from the previous frame
            _rayCaster.ResetCollisions();

            // Debug.Log($"Resolving velocity: {velocity}");

            if (velocity.x != 0)
            {
                _rayCaster.HorizontalRaycast(ref velocity);
            }

            if (velocity.y != 0)
            {
                _rayCaster.VerticalRaycast(ref velocity);
            }

            if (velocity.x != 0 && velocity.y != 0)
            {
                _rayCaster.CornerRayCast(ref velocity);
            }

            //NOTE Why do I have this line?
            _velocity = velocity;

            return _velocity;

            //NOTE Was this doing something?
            // if (isOnPlatform)
            // {
            //     // CollisionInfo.Below = true;
            // }
        }
    }
}