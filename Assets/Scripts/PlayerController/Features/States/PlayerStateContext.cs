// using System;
// using PlayerController.Application.Abstractions;
// using PlayerController.Application.Physics.DataStructures;
// using PlayerController.Features.Physics.DataStructures;
// using UnityEngine;

// namespace PlayerController.Features.States
// {

//     public class PlayerStateContext : IPlayerStateContext
//     {
//         #region Properties
//         private bool _isBouncing;
//         public bool IsBouncing
//         {
//             get
//             {
//                 return _isBouncing;
//             }
//             set
//             {
//                 _isBouncing = value;
//             }
//         }


//         private bool _isSwinging;
//         public bool IsSwinging
//         {
//             get
//             {
//                 return _isSwinging;
//             }
//             set
//             {
//                 _isSwinging = value;
//             }
//         }


//         private bool _isWallSliding;
//         public bool IsWallSliding
//         {
//             get
//             {
//                 return _isWallSliding;
//             }
//             set
//             {
//                 _isWallSliding = value;
//             }
//         }

//         private bool _isWallJumping;
//         public bool IsWallJumping
//         {
//             get
//             {
//                 return _isWallJumping;
//             }
//             set
//             {
//                 if (value && _isWallJumping)
//                 {
//                     _isWallSliding = false;
//                 }
//                 _isWallJumping = value;
//             }
//         }


//         public PlayerState CurrentState => _currentState;
//         private PlayerState _currentState;
//         private PlayerState _playerState;


//         #endregion Properties        private PlayerState _playerState;
//         public Action<PlayerState> OnPlayerStateChanged;
//         private IPhysicsContext _physicsContext;
//         private IPlayerInputContext _inputContext;
//         private bool _onGroundOrPlatform => _physicsContext.CurrentState == PhysicalState.Grounded || _physicsContext.CurrentState == PhysicalState.OnPlatform;


//         // Events
//         public Action OnLanded;


//         public PlayerStateContext(IPhysicsContext physicsContext, IPlayerInputContext inputContext)
//         {
//             _physicsContext = physicsContext;
//             _inputContext = inputContext;
//             // _rBody = _physicsContext.GetComponent<Rigidbody2D>();
//         }

//         public void SetState(PlayerState newState)
//         {
//             NotifyUniqueStateChange(newState);
//             _playerState = newState;
//             _currentState = newState;
//             // OnPlayerStateChanged?.Invoke(_playerState);
//             // Debug.Log($"Setting new state: {_playerState}");
//         }

//         private void NotifyUniqueStateChange(PlayerState newState)
//         {
//             // Emit an event when we go from falling to on a wall or grounded
//             if (_currentState == PlayerState.Fall)
//             {
//                 if (_onGroundOrPlatform)
//                 {
//                     OnLanded?.Invoke();
//                 }
//             }
//         }

//         public void CheckState()
//         {
//             if (CheckIdle())
//             {
//                 _playerState = PlayerState.Idle;
//             }
//             else if (CheckRunning())
//             {
//                 _playerState = PlayerState.Run;
//             }
//             else if (CheckSlipping())
//             {
//                 _playerState = PlayerState.Slip;
//             }
//             else if (CheckSliding())
//             {
//                 _playerState = PlayerState.Slide;
//             }
//             else if (CheckFalling())
//             {
//                 _playerState = PlayerState.Fall;
//             }
//             else if (CheckJumping())
//             {
//                 _playerState = PlayerState.Jump;
//             }
//             else if (CheckWallJumping())
//             {
//                 _playerState = PlayerState.WallJump;
//             }
//             else if (CheckWallSliding())
//             {
//                 _playerState = PlayerState.WallSlide;
//             }
//             else if (CheckSwinging())
//             {
//                 _playerState = PlayerState.Swing;
//             }
//             else if (CheckBouncing())
//             {
//                 _playerState = PlayerState.Bounce;
//             }

//             if (_playerState != CurrentState)
//             {
//                 SetState(_playerState);
//             }

//         }

//         private bool CheckIdle()
//         {
//             if (_inputContext.VectorX == 0 && _onGroundOrPlatform)
//             {
//                 return true;
//             }
//             return false;
//         }

//         private bool CheckRunning()
//         {
//             if (_inputContext.VectorX != 0 && _onGroundOrPlatform && !CheckSlipping() && !CheckSliding())
//             {
//                 return true;
//             }
//             return false;
//         }

//         private bool CheckFalling()
//         {
//             if (_physicsContext.CurrentState == PhysicalState.Falling && !CheckSwinging())
//             {
//                 return true;
//             }
//             return false;
//         }

//         private bool CheckJumping()
//         {
//             // Debug.Log($"CurrentState: {_physicsContext.CurrentState}; Is NOT Swinging: {!CheckSwinging()}");
//             if (_physicsContext.CurrentState == PhysicalState.Rising && (!CheckSwinging() || !CheckBouncing()))
//             {
//                 return true;
//             }
//             return false;
//         }

//         private bool CheckWallSliding()
//         {
//             return IsWallSliding;
//         }

//         private bool CheckSwinging()
//         {
//             return IsSwinging;
//         }

//         private bool CheckWallJumping()
//         {
//             return IsWallJumping;
//         }

//         private bool CheckSlipping()
//         {
//             // // Ensure you have input AND the player isnt moving verrrrrry slowly
//             // if (_inputContext.VectorX == 0) return false;

//             // // float velX =
//             // float dir = Mathf.Sign(_inputContext.VectorX);
//             // float momentum = Mathf.Sign(_rBody.linearVelocityX);


//             // // if the player is grounded
//             // if (_physicsContext.CurrentState == PhysicalState.Grounded || _physicsContext.CurrentState == PhysicalState.OnPlatform)
//             // {
//             //     // if the player's input does not match their momentum vector
//             //     if (dir != momentum && (dir != 0) && (momentum != 0))
//             //     {
//             //         // Debug.Log($"[PlayerStatContext] dir: = {_inputContext.VectorX}, momentum = {_rBody.linearVelocityX}");
//             //         _prevVelocity = _rBody.linearVelocityX;
//             //         return true;
//             //     }
//             // }
//             return false;
//         }

//         private bool CheckSliding()
//         {
//             if (_inputContext.SlidePressed && _onGroundOrPlatform)
//             {
//                 return true;
//             }
//             else if (_physicsContext.SlideOverride)
//             {
//                 return true;
//             }
//             return false;
//         }

//         private bool CheckBouncing()
//         {
//             return IsBouncing;
//         }

//         public void Tick()
//         {
//             CheckState();
//             // Debug.Log($"Player state: {CurrentState}");
//         }
//     }
// }