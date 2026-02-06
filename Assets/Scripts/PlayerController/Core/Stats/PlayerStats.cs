using Codice.CM.SEIDInfo;
using NUnit.Framework.Constraints;
using PlayerController.Application.Abstractions;

namespace PlayerController.Core.Stats
{
    public struct PlayerStats
    {
        // public Stat GroundSpeedLimit;
        public Stat RunSpeed;
        public Stat RunAccel;
        public Stat RunAccelTime;
        public Stat BrakeAccel;
        public Stat SlowFall;
        public Stat FastFall;
        public Stat AerialAccel;
        // public Stat AerialAccelTime;
        public Stat AerialBrake;
        public Stat JumpHeight;
        public Stat JumpApexTime;
        public Stat WallSlideSpeed;
        public Stat WallJumpHeight;
        public Stat WallJumpVelocity;
        public Stat WallJumpApexTime;
        public Stat SprintSpeed;
        public Stat SprintAccel;
        // public Stat QuickStepSpeed;
        public Stat QuickStepDistance;

        // public Stat SlideBoost;
        // public Stat SlideBrakeForce;
        // public Stat GrabBufferTime;
        // public Stat GrabRadius;
        public Stat JumpBufferTime;
        public Stat WallJumpBufferTime;
        public Stat WallJumpDistance;
        public Stat CoyoteTime;
        public Stat BaseGravity;
        // public Stat JumpGravity;
        public Stat TotalJumps;

        // public PlayerStats(bool yes = true)
        // {
        //     RunSpeed = new Stat(15f);
        //     RunAccel = new Stat(5f);
        //     BrakeAccel = new Stat(30f);
        //     SlowFall = new Stat(5f);
        //     FastFall = new Stat(9f);
        //     AerialAccel = new Stat(4f);
        //     AerialBrake = new Stat(30f);
        //     JumpHeight = new Stat(4.4f);
        //     JumpApexTime = new Stat(.4f);
        //     WallSlideSpeed = new Stat(-5f);
        //     WallJumpHeight = new Stat(4f);
        //     WallJumpVelocity = new Stat(10f);
        //     WallJumpApexTime = new Stat(.3f);
        //     JumpBufferTime = new Stat(0.1f);
        //     WallJumpBufferTime = new Stat(0.5f);
        //     CoyoteTime = new Stat(0.1f);
        //     SprintSpeed = new Stat(20f);
        //     SprintAccel = new Stat(10f);
        //     AerialAccelTime = new Stat(0.2f);
        //     WallJumpDistance = new Stat(2f);
        //     RunAccelTime = new Stat(0.1f);
        //     BaseGravity = new Stat(-9.81f);
        //     JumpGravity = new Stat(0);
        // }

        public PlayerStats(IPlayerMovementStats stats)
        {
            SprintSpeed = new Stat(stats.SprintSpeed);
            SprintAccel = new Stat(stats.SprintAccel);

            RunSpeed = new Stat(stats.RunSpeed);
            RunAccel = new Stat(stats.RunAccel);
            RunAccelTime = new Stat(stats.RunAccel);
            BrakeAccel = new Stat(stats.BrakeAccel);

            SlowFall = new Stat(stats.SlowFall);
            FastFall = new Stat(stats.FastFall);
            AerialAccel = new Stat(stats.AerialAccel);
            // AerialAccelTime = new Stat(stats.AerialAccelTime);
            AerialBrake = new Stat(stats.AerialBrake);

            JumpHeight = new Stat(stats.JumpHeight);
            JumpApexTime = new Stat(stats.JumpApexTime);
            TotalJumps = new Stat(stats.TotalJumps);

            WallSlideSpeed = new Stat(stats.WallSlideSpeed);

            WallJumpHeight = new Stat(stats.WallJumpHeight);
            WallJumpVelocity = new Stat(stats.WallJumpVelocity);
            WallJumpApexTime = new Stat(stats.WallJumpApexTime);
            WallJumpDistance = new Stat(stats.WallJumpDistance);

            CoyoteTime = new Stat(stats.CoyoteTime);
            JumpBufferTime = new Stat(stats.JumpBufferTime);
            WallJumpBufferTime = new Stat(stats.WallJumpBufferTime);

            BaseGravity = new Stat(stats.BaseGravity);
            // JumpGravity = new Stat(stats.JumpGravity);

            // QuickStepSpeed = new Stat(stats.QuickStepSpeed);
            QuickStepDistance = new Stat(stats.QuickStepDistance);
        }
    }


}