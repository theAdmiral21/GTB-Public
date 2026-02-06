using UnityEngine;

namespace PlayerController.Application.Physics.DataStructures
{
    public struct PhysicsStateValues
    {
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public float Gravity;
        public bool StopGravity;
        public float BlockXCounter;
        public float deltaTime;
    }
}