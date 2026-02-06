using UnityEngine;

namespace PlayerController.Application.Movement.DataStructures
{
    [System.Serializable]
    public struct KinematicResult
    {
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public float Gravity;
        public float Dt;
    }
}