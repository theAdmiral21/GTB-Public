using PlayerController.Features.Animations;
using UnityEngine;

namespace PlayerController.Features.Animations.AnimatorStates
{
    public class SwingState : BaseAnimatorState
    {


        public SwingState(Animator animator) : base(animator)
        {
        }

        public override void Enter()
        {
            // Debug.Log("Entering slip/slide state");
            _animator.SetBool("isSwinging", true);
        }

        public override void Exit()
        {
            _animator.SetBool("isSwinging", false);
        }

        public override void Update()
        {
        }
    }
}