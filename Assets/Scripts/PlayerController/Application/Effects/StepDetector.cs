using PlayerController.Core.Effects.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Stats;
using Primitives.Audio;
using Primitives.Audio.Enums;
using UnityEngine;

namespace PlayerController.Application.Effects
{
    public class StepDetector
    {
        private float _accumulatedDistance = 0f;
        private PlayerStats _stats;

        public StepDetector(PlayerStats stats)
        {
            _stats = stats;
        }

        public StepEffect TryStep(PhysicsContext physicsContext, float stepDistance)
        {
            // Accumulate distance only when grounded.
            if (physicsContext.IsGrounded)
            {
                _accumulatedDistance += physicsContext.DeltaPosition.magnitude;
                if (_accumulatedDistance > stepDistance)
                {
                    _accumulatedDistance -= stepDistance;

                    float speed = NormalizeSpeed(physicsContext.Velocity.x);
                    return Approved(physicsContext.Surface, speed);
                }
                return Denied();
            }
            else
            {
                // Reset the distance traveled when airborne.
                _accumulatedDistance = 0;
                return Denied();
            }
        }

        private float NormalizeSpeed(float runSpeed)
        {
            if (runSpeed < _stats.SprintSpeed.Value)
            {
                return runSpeed / _stats.RunSpeed.Value;
            }
            return runSpeed / _stats.SprintSpeed.Value;
        }

        private StepEffect Approved(SurfaceType surface, float speed01)
        {
            // Debug.Log($"Step effect approved");
            if (speed01 < .25)
            {
                return new StepEffect(true, PlayerSoundKey.Walk, surface, speed01);
            }
            else
            {
                return new StepEffect(true, PlayerSoundKey.Run, surface, speed01);
            }
        }

        private StepEffect Denied()
        {
            // Debug.Log($"Step effect denied");
            return new StepEffect(false, PlayerSoundKey.None, SurfaceType.None, 0);
        }
    }
}