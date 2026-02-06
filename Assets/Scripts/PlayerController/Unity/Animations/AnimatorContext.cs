using PlayerController;
using UnityEngine;
using PlayerController.Features.Animations.AnimatorStates;
using System;
using PlayerController.Application.Physics.DataStructures;

namespace PlayerController.Features.Animations
{
    public class AnimatorContext
    {
        private Animator _animator;
        private IdleState _idleState;
        private RunState _runState;
        private FallState _fallState;
        private JumpState _jumpState;
        private SlideState _slideState;
        private WallSlideState _wallSlideState;
        private WallJumpState _wallJumpState;
        private SwingState _swingState;
        // private BounceState _bounceState;
        public BaseAnimatorState CurrentState => _currentState;
        private BaseAnimatorState _currentState;
        private PlayerState _playerState;
        private Transform _parentTransform;

        public Action DirectionFlipped;


        // public AnimationEvents AnimationEvents;
        public AnimatorContext(Animator animator)
        {
            // _playerHost = playerHost;
            // PlayerStateContext playerStateContext = _playerHost.StateSystem.StateContext;
            _animator = animator;
            // _animator.runtimeAnimatorController = playerHost.SessionData.CharacterData.characterAnimator;

            // _parentTransform = playerHost.GetComponent<Transform>();

            _idleState = new IdleState(_animator);
            _runState = new RunState(_animator);
            _fallState = new FallState(_animator);
            _jumpState = new JumpState(_animator);
            _slideState = new SlideState(_animator);
            _wallSlideState = new WallSlideState(_animator);
            _wallJumpState = new WallJumpState(_animator);
            _swingState = new SwingState(_animator);
            // _bounceState = new BounceState(_animator, playerStateContext);
            _currentState = _idleState;
        }
        public void Subscribe()
        {
            // _playerHost.StateSystem.StateContext.OnPlayerStateChanged += SetState;
        }

        public void Unsubscribe()
        {
            // _playerHost.StateSystem.StateContext.OnPlayerStateChanged -= SetState;
        }
        public void ChangeState(BaseAnimatorState newState)
        {
            if (newState == _currentState) return;
            // Debug.Log($"Animator state changed from: {_currentState} to {newState}");
            _currentState.Exit();
            newState.Enter();
            _currentState = newState;
        }

        private void SetState(PlayerState newPlayerState)
        {
            // Debug.Log($"Animator received state: {newPlayerState}");
            _playerState = newPlayerState;
            // Start simple
            if (_playerState == PlayerState.Idle)
            {
                ChangeState(_idleState);
            }
            else if (_playerState == PlayerState.Run)
            {
                ChangeState(_runState);
            }
            else if (_playerState == PlayerState.Slip)
            {
                ChangeState(_slideState);
            }
            else if (_playerState == PlayerState.Slide)
            {
                ChangeState(_slideState);
            }
            else if (_playerState == PlayerState.Jump)
            {
                ChangeState(_jumpState);
            }
            else if (_playerState == PlayerState.Fall)
            {
                ChangeState(_fallState);
            }
            else if (_playerState == PlayerState.WallJump)
            {
                ChangeState(_wallJumpState);
            }
            else if (_playerState == PlayerState.WallSlide)
            {
                ChangeState(_wallSlideState);
            }
            else if (_playerState == PlayerState.Swing)
            {
                ChangeState(_swingState);
            }
            else if (_playerState == PlayerState.Bounce)
            {
                // ChangeState(_bounceState);
            }
        }
        public void Tick()
        {
            _currentState.Update();
        }
        public void FixedTick()
        {
            AutoFlip();
        }

        private void AutoFlip()
        {
            // Method that flips the player based off of their velocity.
            if (_currentState == _swingState) { return; }
            if (_currentState == _slideState) { return; }
            // float velX = _playerHost.MoveSystem.MoveController.Velocity.x;
            // float velSign = Mathf.Sign(velX);

            // if (velX != 0)
            // {
            //     if (velSign != 0f)
            //     {
            //         _parentTransform.localScale = new Vector3(velSign, _parentTransform.localScale.y, _parentTransform.localScale.z);
            //         DirectionFlipped?.Invoke();
            //     }
            // }
        }


    }
}