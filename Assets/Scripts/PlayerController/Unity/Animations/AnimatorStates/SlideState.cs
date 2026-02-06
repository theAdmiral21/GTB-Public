using PlayerController.Features.Animations;
using UnityEngine;

namespace PlayerController.Features.Animations.AnimatorStates
{
    public class SlideState : BaseAnimatorState
    {
        public SlideState(Animator animator) : base(animator)
        {
        }

        public override void Enter()
        {
            // Debug.Log("Entering slip/slide state");
            _animator.SetBool("isSlipping", true);
        }

        public override void Exit()
        {
            _animator.SetBool("isSlipping", false);
        }

        public override void Update()
        {
        }
    }
}