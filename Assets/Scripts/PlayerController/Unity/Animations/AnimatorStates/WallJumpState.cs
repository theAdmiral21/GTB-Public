using PlayerController.Features.Animations;
using UnityEngine;

namespace PlayerController.Features.Animations.AnimatorStates
{
    public class WallJumpState : BaseAnimatorState
    {

        public WallJumpState(Animator animator) : base(animator)
        {

        }

        public override void Enter()
        {
            _animator.SetBool("isJumping", true);
        }

        public override void Exit()
        {
            _animator.SetBool("isJumping", false);
        }

        public override void Update()
        {
        }
    }
}