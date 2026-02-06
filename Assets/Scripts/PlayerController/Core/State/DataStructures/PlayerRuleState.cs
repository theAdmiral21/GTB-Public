using UnityEngine;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Core.Inputs.DataStructures;

namespace PlayerController.Core.State.DataStructures
{
    [System.Serializable]
    public class PlayerRuleState
    {
        public float RemainingJumps;
        private float _totalAllowedJumps;
        public bool IsDisabled;
        public bool ApplyGravity;
        public FallType FallType;
        public float Dt;
        public float Dir = 1;
        public bool LandingStopRequested;
        public bool GroundedLastFrame;
        /*
        =======================================================================================
        |                               Timer fields/props                                    |
        =======================================================================================
        */

        // Coyote jumping
        public bool CanCoyoteJump => _coyoteCounter > 0;
        private readonly float _coyoteTimer;
        private float _coyoteCounter;

        // X input lock - use primarily for wall jumping and quick stepping
        public bool XInputLocked => _blockXCounter > 0;
        private readonly float _blockXTime;
        private float _blockXCounter;

        // Buffered jump
        public bool BufferLatch;
        public bool JumpBuffered => _jumpBufferCounter > 0;
        private readonly float _jumpBufferTime;
        private float _jumpBufferCounter;

        // Ungrounded timer
        public float UngroundedCounter => _ungroundedCounter;
        private float _ungroundedCounter;
        public readonly float JumpApexTime;

        // Jump cool down timer
        public bool JumpCoolDownActive => _jumpCoolDownCounter > 0;
        private readonly float _jumpCoolDownTime;
        private float _jumpCoolDownCounter;

        public PlayerRuleState(PlayerStats stats)
        {
            _totalAllowedJumps = stats.TotalJumps.Value;
            RemainingJumps = _totalAllowedJumps;
            JumpApexTime = stats.JumpApexTime.Value;

            _coyoteTimer = stats.CoyoteTime.Value;
            _coyoteCounter = 0;

            _jumpBufferTime = stats.JumpBufferTime.Value;
            _jumpBufferCounter = 0;

            // _coolDownTime = stats.JumpCoolDownTime.value;
            // _jumpCoolDownCounter = 0;

            IsDisabled = false;
            ApplyGravity = true;
            FallType = FallType.None;

            _blockXTime = stats.WallJumpApexTime.Value;
            _blockXCounter = 0;

            Dt = 0;


        }
        public void UpdateRules(InputState inputValues, PhysicsContext physicsContext, float dt)
        {
            // Update dt
            Dt = dt;
            // Set the direction the player is facing
            SetDirection(inputValues, physicsContext);
            // Reset the jumps
            ResetJumps(physicsContext);
            // Tick active timers
            TickTimers();

            // I think it is better to start timers after they've updated so that they don't exit prematurely because they're being updated before they're being evaluated.

            // Start the coyote timer
            StartCoyoteTimer(physicsContext);
            // Start or update Ungrounded timer
            StartUngroundedTimer(physicsContext);
        }

        public void StartBlockXTimer()
        {
            _blockXCounter = _blockXTime;
            // Debug.Log("Started BlockXTimer");
        }

        public void StartCoyoteTimer(PhysicsContext physicsContext)
        {
            // Start coyote timer
            // Debug.Log($"GroundedLastFrame: {GroundedLastFrame} Not Grounded: {!physicsContext.IsGrounded} IsFalling: {physicsContext.IsFalling} IsRising: {physicsContext.IsRising} IsFloating: {physicsContext.IsFloating}");

            // For some reason my fall trigger is a little slow, but the rising trigger is sensitive enough to prevent jumping from triggering this.
            if (GroundedLastFrame && !physicsContext.IsGrounded && !physicsContext.IsRising)
            {
                _coyoteCounter = _coyoteTimer;
                // Debug.Log("Started coyote timer");
            }
        }

        public void StartJumpBufferTimer()
        {

            _jumpBufferCounter = _jumpBufferTime;
            // Debug.Log("Started jump buffer timer");
        }


        public void ResetBufferTimer()
        {
            // Debug.Log($"Reset buffer timer - Frame: {Time.frameCount}");
            _jumpBufferCounter = 0;
        }

        public void StartUngroundedTimer(PhysicsContext physicsContext)
        {
            if (!physicsContext.IsGrounded)
            {
                _ungroundedCounter += Dt;
            }
            else
            {
                _ungroundedCounter = 0;
            }
        }

        public void SetDirection(InputState input, PhysicsContext physicsContext)
        {
            float velX = physicsContext.Velocity.x;
            float velSign = Mathf.Sign(velX);

            if (velX != 0)
            {
                if (velSign != 0f)
                {
                    Dir = velSign;
                }
            }
        }
        public void ResetJumps(PhysicsContext physicsContext)
        {
            if (physicsContext.IsGrounded && !GroundedLastFrame)
            {
                // Give the player their jumps back when they hit the ground
                RemainingJumps = _totalAllowedJumps;
                // Reset the buffer timer so it doesn't keep firing after the player lands.
                ResetBufferTimer();
            }
        }
        private void TickTimers()
        {
            // Tick your timers with this dt
            if (_blockXCounter > 0)
            {
                _blockXCounter -= Dt;
            }
            if (_coyoteCounter > 0)
            {
                _coyoteCounter -= Dt;
            }
            if (_jumpBufferCounter > 0)
            {
                _jumpBufferCounter -= Dt;
            }
        }

    }
}