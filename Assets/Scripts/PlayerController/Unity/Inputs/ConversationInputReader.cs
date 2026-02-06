using System;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Core.Movement.DataStructures;
using Primitives.Input;
using Primitives.Input.Abstractions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController.Unity.Inputs
{
    public class ConversationInputReader : BaseInputReader, GameInputs.IInConversationActions
    {
        public override InputContext Type => InputContext.Conversation;
        public override bool IsActive => _actions.InConversation.enabled;
        public override void Initialize(GameInputs inputActions)
        {
            _actions = inputActions;
            _actions.InConversation.SetCallbacks(this);
            // Debug.Log($"{this} has input action asset: {_actions}");
            Deactivate();
            // Debug.Log($"{name} is active: {IsActive}");
        }
        public override void Activate() => _actions.InConversation.Enable();
        public override void Deactivate() => _actions.InConversation.Disable();
        public void OnCompleteDialog(InputAction.CallbackContext context)
        {
            Debug.Log($"Complete dialog received!");
        }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            Debug.Log($"Navigate dialog received!");
        }

        public void OnSelectDialog(InputAction.CallbackContext context)
        {
            Debug.Log($"Select dialog received!");
        }


    }
}