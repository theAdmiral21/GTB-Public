using UnityEngine;

namespace PlayerController.Features.Animations
{
    public abstract class BaseAnimatorState
    {
        protected Animator _animator;
        public BaseAnimatorState(Animator animator)
        {
            _animator = animator;
        }
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}