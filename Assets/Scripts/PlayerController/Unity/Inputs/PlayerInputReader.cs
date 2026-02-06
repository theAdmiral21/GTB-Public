using System;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Core.Movement.DataStructures;
using Primitives.Input;
using Primitives.Input.Abstractions;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

/*
Optional actions to add later:
- air dodge
- quickstep
- pounce
- smash style dash
- fast fall
*/

namespace PlayerController.Unity.Inputs
{
    public class PlayerInputReader : BaseInputReader, GameInputs.IInGameActions
    {
        public override InputContext Type => InputContext.Gameplay;
        public Vector2 MoveInput => _moveInput;
        private Vector2 _moveInput = Vector2.zero;

        // Properties for building the current input state
        public Vector2 LeftStick => _actions.InGame.XInput.ReadValue<Vector2>();
        public bool SprintPressed =>
        (_actions.InGame.Sprint.phase == InputActionPhase.Started) ||
        (_actions.InGame.Sprint.phase == InputActionPhase.Performed);

        public bool JumpPressed => _actions.InGame.Jump.phase == InputActionPhase.Started;

        public bool HoldingJump => _actions.InGame.Jump.phase == InputActionPhase.Performed;

        public bool GrabPressed =>
        (_actions.InGame.Grab.phase == InputActionPhase.Started) ||
        (_actions.InGame.Grab.phase == InputActionPhase.Performed);

        public bool BarkPressed => _actions.InGame.Bark.phase == InputActionPhase.Started;
        public bool BarkHeld => _actions.InGame.Bark.phase == InputActionPhase.Performed;

        // Event actions
        public event Action<IActionRequest> JumpCancel;
        public event Action<IActionRequest> Run;
        public event Action<IActionRequest> RunStop;
        public event Action<IActionRequest> QuickStep;
        public event Action<IActionRequest> Bark;
        public event Action<IActionRequest> Pause;

        // Buffered events
        public event Action<IActionRequest> BufferJump;

        // Input timers
        const float DOUBLE_TAP_WINDOW = 0.25f;
        private float _prevTap;
        private float _lastTapTime;

        public override bool IsActive => _actions.InGame.enabled;

        public override void Initialize(GameInputs inputActions)
        {
            _actions = inputActions;
            _actions.InGame.SetCallbacks(this);
            // Debug.Log($"{this} has input action asset: {_actions}");
            Deactivate();
            // Debug.Log($"{name} is active: {IsActive}");
        }


        public override void Activate() => _actions.InGame.Enable();
        public override void Deactivate() => _actions.InGame.Disable();

        public void OnJump(InputAction.CallbackContext context)
        {
            // Debug.Log($"Jump pressed");
            if (context.started)
            {
                // Debug.Log($"Recived jump input - Frame: {Time.frameCount}");
                BufferJump?.Invoke(new JumpRequest(true));
            }
            else if (context.canceled)
            {
                // Debug.Log($"Recived jump cancel input");
                JumpCancel?.Invoke(new JumpCancelRequest { Requested = true });
            }
        }
        public void OnXInput(InputAction.CallbackContext context)
        {
            // Debug.Log($"Got x input");
            if (context.started)
            {
                float tapDir = Mathf.Sign(context.ReadValue<Vector2>().x);
                if (Mathf.Abs(tapDir) < 0.5f) return;
                if (tapDir == _prevTap && (Time.time - _lastTapTime < DOUBLE_TAP_WINDOW))
                {
                    // Debug.Log("Got quick step!");
                    QuickStep?.Invoke(new QuickStepRequest(true, tapDir));
                }
                // Get the tap time
                _lastTapTime = Time.time;
                _prevTap = tapDir;
            }
            else if (context.performed)
            {
                // Debug.Log($"Recived run input");
                OnMove(context);
            }
            else if (context.canceled)
            {
                OnStopMove(context);
            }
        }
        private void OnMove(InputAction.CallbackContext context)
        {
            if (context.ReadValue<Vector2>() != _moveInput)
            {
                _moveInput = context.ReadValue<Vector2>();
            }
        }
        private void OnStopMove(InputAction.CallbackContext context)
        {
            // Debug.Log($"Recived run stop input");
            RunStop?.Invoke(new RunStopRequest(true, context.ReadValue<Vector2>()));
            _moveInput.x = 0;
        }

        private void Update()
        {
            if (MoveInput != Vector2.zero)
            {
                Run?.Invoke(new RunRequest(true, MoveInput));
            }
        }
        public void OnBark(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                // Debug.Log($"Emitted bark request - frame {Time.frameCount}");
                Bark?.Invoke(new BarkRequest(true));
            }

        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Debug.Log("Pause requested");
                Pause?.Invoke(new PauseRequest());
            }
        }

        public void OnSprint(InputAction.CallbackContext context) { }

        public void OnLookUp(InputAction.CallbackContext context) { }

        public void OnHowl(InputAction.CallbackContext context) { }


        public void OnGrab(InputAction.CallbackContext context) { }

        public void OnDrop(InputAction.CallbackContext context) { }

        public void OnDebugRespawn(InputAction.CallbackContext context) { }

        public void OnYInput(InputAction.CallbackContext context) { }

        public void OnDown(InputAction.CallbackContext context) { }

        public void OnChangeMoveMode(InputAction.CallbackContext context) { }

        public void OnHideDebugInfo(InputAction.CallbackContext context) { }

        public void OnRightTriggerPull(InputAction.CallbackContext context) { }

        public void OnLeftTriggerPull(InputAction.CallbackContext context) { }

        public void OnWallJump(InputAction.CallbackContext context) { }

        public void OnSlide(InputAction.CallbackContext context) { }
    }
}