using System.Numerics;

namespace PlayerController.Features.Movement.DataStructures
{
    public struct KinematicState
    {
        Vector2 CurrentVelocity;
        Vector2 CurrentAcceleration;
        float Gravity;
    }
}