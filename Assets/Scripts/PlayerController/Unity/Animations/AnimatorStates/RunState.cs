using PlayerController.Features.Animations;
using UnityEngine;

namespace PlayerController.Features.Animations.AnimatorStates
{
    public class RunState : BaseAnimatorState
    {
        public RunState(Animator animator) : base(animator)
        {
        }

        public override void Enter()
        {
            _animator.SetBool("running", true);
        }

        public override void Exit()
        {
            _animator.SetBool("running", false);
        }

        public override void Update()
        {
        }
    }
}