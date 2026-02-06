using System;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Core.Movement.DataStructures;
using Primitives.Input;
using Primitives.Input.Abstractions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerController.Unity.Inputs
{
    public abstract class BaseInputReader : MonoBehaviour
    {
        public abstract InputContext Type { get; }
        public abstract bool IsActive { get; }

        // Input actions
        protected GameInputs _actions;
        // public void SetInputActionAsset(GameInputs inputActions)
        // {
        //     _actions = inputActions;
        // }

        // public void OnEnable()
        // {
        //     _actions.Enable();
        // }

        // public void OnDisable()
        // {
        //     _actions.Disable();
        // }

        public abstract void Activate();

        public abstract void Deactivate();

        public abstract void Initialize(GameInputs inputActions);
    }
}