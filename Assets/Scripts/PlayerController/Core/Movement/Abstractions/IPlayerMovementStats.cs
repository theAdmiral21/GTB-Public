using Unity.Profiling;

namespace PlayerController.Application.Abstractions
{
    public interface IPlayerMovementStats
    {
        public float SprintSpeed { get; }
        public float SprintAccel { get; }
        public float GroundSpeedLimit { get; }
        public float RunSpeed { get; }
        public float RunAccel { get; }
        public float BrakeAccel { get; }
        public float SlowFall { get; }
        public float FastFall { get; }
        public float AerialAccel { get; }
        public float AerialBrake { get; }
        public float JumpHeight { get; }
        public float JumpApexTime { get; }
        public float WallSlideSpeed { get; }
        public float WallJumpHeight { get; }
        public float WallJumpVelocity { get; }
        public float WallJumpApexTime { get; }
        public float WallJumpDistance { get; }
        public float SlideBoost { get; }
        public float SlideBrakeForce { get; }
        public float GrabBufferTime { get; }
        public float GrabRadius { get; }
        public float SwingJumpForce { get; }
        public float JumpBufferTime { get; }
        public float WallJumpBufferTime { get; }
        public float CoyoteTime { get; }
        public float BoostCoolDown { get; }
        public float BaseGravity { get; }
        public float QuickStepDistance { get; }
        public int TotalJumps { get; }
    }
}