using PlayerController.Features.Animations;
using UnityEngine;

namespace PlayerController.Features.Animations.AnimatorStates
{
    public class FallState : BaseAnimatorState
    {
        public FallState(Animator animator) : base(animator)
        {
        }

        public override void Enter()
        {
            _animator.SetBool("isFalling", true);
        }

        public override void Exit()
        {
            _animator.SetBool("isFalling", false);
        }

        public override void Update()
        {
        }
    }
}