using UnityEngine;
using PlayerController.Core.Physics.DataStructures;
using Primitives.Audio.Enums;

namespace PlayerController.Core.State.DataStructures
{
    [System.Serializable]
    public struct PhysicsContext
    {
        public bool IsGrounded;
        public bool IsRising;
        public bool IsFalling;
        public bool IsOnPlatform;
        public bool IsTouchingWall;
        public WallContact WallContactType;
        public bool IsWallSliding;
        public bool IsHanging;
        public bool IsFloating;
        public bool IsSliding;
        public bool Heading;
        public Vector2 Velocity => _velocity;
        private Vector2 _velocity;
        public ContactType FloorContactType;
        public SurfaceType Surface;
        public Vector3 DeltaPosition;

        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }
    }
}
