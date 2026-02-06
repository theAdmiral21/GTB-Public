using PlayerController.Application.Effects.Abstractions;
using PlayerController.Application.Effects.DataStructures;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController.Unity.Animations
{
    public class AnimatorEffectBridge : MonoBehaviour, IPlayerAnimator
    {
        [SerializeField] private Transform _animatorTransform;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerMovementSO _playerMovementSO;
        private Vector3 _transformCache = Vector3.one;
        // private float _xVisualOffset = 0.2f;
        public PlayerAnimatorState CurrentState => _currentState;
        private PlayerAnimatorState _currentState;

        public void SyncAnimation(in InputState inputValue, in PhysicsContext physicsContext, in PlayerRuleState ruleState)
        {
            // Set the direction of the animator.
            _transformCache.x = ruleState.Dir;
            _animatorTransform.localScale = _transformCache;
            // _animatorTransform.localPosition = _xVisualOffset * ruleState.Dir * Vector3.right;

            // Evaluate the easy state stuff
            _animator.SetBool("isGrounded", physicsContext.IsGrounded);
            _animator.SetBool("isFalling", physicsContext.IsFalling);
            _animator.SetBool("isWallsliding", physicsContext.IsWallSliding);

            // evaluate the booleans to determine what is going on
            if (physicsContext.IsGrounded && inputValue.LeftStick.x != 0)
            {
                // Debug.Log("Animating run");
                _animator.SetBool("isRunning", true);
                _animator.SetFloat("runSpeed", Mathf.Abs(inputValue.LeftStick.x) / 1f);
            }
            else
            {
                _animator.SetBool("isRunning", false);
            }

            if (physicsContext.IsGrounded && physicsContext.Velocity.y < 0f)
            {
                _animator.SetBool("isFalling", true);
            }
            else
            {
                _animator.SetBool("isFalling", false);
            }
        }

        public void ApplyEffect(IEffectResult effect)
        {
            switch (effect)
            {
                case BarkEffect bark:
                    {
                        // Debug.Log("Animating bark");
                        _animator.SetTrigger("barkTrigger");
                        break;
                    }
                case HowlEffect howl:
                    {
                        Debug.Log("Animating howl");
                        break;
                    }
                case JumpEffect jump:
                    {
                        // Debug.Log("Animating jump");
                        _animator.SetTrigger("jumpTrigger");
                        break;
                    }
                case LandEffect landing:
                    {
                        // Debug.Log("Animating landing");
                        break;
                    }
            }
        }
    }
}