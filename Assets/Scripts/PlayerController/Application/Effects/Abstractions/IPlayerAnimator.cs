using PlayerController.Application.Effects.DataStructures;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Stats;

namespace PlayerController.Application.Effects.Abstractions
{
    public interface IPlayerAnimator
    {
        public PlayerAnimatorState CurrentState { get; }

        /// <summary>
        /// Method for animating continuous movement. Think running, falling, idle, etc.
        /// </summary>
        /// <param name="physicsContext"></param>
        /// <param name="ruleState"></param>
        public void SyncAnimation(in InputState inputValues, in PhysicsContext physicsContext, in PlayerRuleState ruleState);

        /// <summary>
        /// Method for animation one-shot animations. Think jumping, landing, barking, pouncing, etc.
        /// </summary>
        /// <param name="effect"></param>
        public void ApplyEffect(IEffectResult effect);

    }
}