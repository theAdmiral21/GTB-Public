using System;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Core.Movement.DataStructures;
using Primitives.Input;
using Primitives.Input.Abstractions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController.Unity.Inputs
{
    public class CutSceneInputReader : BaseInputReader, GameInputs.IInCutSceneActions
    {
        public override InputContext Type => InputContext.CutScene;
        public override bool IsActive => _isActive;
        private bool _isActive => _actions.InCutScene.enabled;
        public override void Initialize(GameInputs inputActions)
        {
            _actions = inputActions;
            _actions.InCutScene.SetCallbacks(this);
            // Debug.Log($"{this} has input action asset: {_actions}");
            Deactivate();
            // Debug.Log($"{name} is active: {IsActive}");
        }
        public override void Activate() => _actions.InCutScene.Enable();
        public override void Deactivate() => _actions.InCutScene.Disable();
        public void OnPauseCutScene(InputAction.CallbackContext context)
        {
            Debug.Log($"Pause cut scene received!");
        }

        public void OnSkipCutScene(InputAction.CallbackContext context)
        {
            Debug.Log($"Skip cut scene received!");
        }



    }
}