using PlayerController.Features.Animations;
using UnityEngine;

namespace PlayerController.Features.Animations.AnimatorStates
{
    public class IdleState : BaseAnimatorState
    {
        public IdleState(Animator animator) : base(animator)
        {
        }

        public override void Enter()
        {
            // Debug.Log("Entering Idle state");
            _animator.SetBool("idle", true);
        }

        public override void Exit()
        {
            _animator.SetBool("idle", false);
        }

        public override void Update()
        {

        }
    }
}