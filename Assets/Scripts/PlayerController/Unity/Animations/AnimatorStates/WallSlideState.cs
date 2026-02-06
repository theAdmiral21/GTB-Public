using PlayerController.Features.Animations;
using UnityEngine;

namespace PlayerController.Features.Animations.AnimatorStates
{
    public class WallSlideState : BaseAnimatorState
    {
        public WallSlideState(Animator animator) : base(animator)
        {
        }

        public override void Enter()
        {
            _animator.SetBool("isOnWall", true);
        }

        public override void Exit()
        {
            _animator.SetBool("isOnWall", false);
        }

        public override void Update()
        {
        }
    }
}