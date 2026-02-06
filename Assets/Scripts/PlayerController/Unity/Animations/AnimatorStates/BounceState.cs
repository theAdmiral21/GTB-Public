// using UnityEngine;

// namespace PlayerController.Features.Animations.AnimatorStates
// {
//     public class BounceState : BaseAnimatorState
//     {
//         private PlayerStateContext _stateContext;

//         public BounceState(Animator animator, PlayerStateContext stateContext) : base(animator)
//         {
//             _stateContext = stateContext;
//         }

//         public override void Enter()
//         {
//             _stateContext.IsBouncing = true;
//             _animator.SetBool("isJumping", true);
//         }

//         public override void Exit()
//         {
//             _stateContext.IsBouncing = false;
//             _animator.SetBool("isJumping", false);
//         }

//         public override void Update()
//         {
//         }
//     }
// }