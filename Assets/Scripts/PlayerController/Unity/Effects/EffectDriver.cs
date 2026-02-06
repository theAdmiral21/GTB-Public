using System.Collections.Generic;
using PlayerController.Application.Abstractions;
using PlayerController.Application.Movement.Abstractions;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Features.Movement;
using PlayerController.Unity.Animations;
using PlayerController.Unity.Audio;
using PlayerController.Unity.Movement;
using Primitives.Audio;
using Primitives.Audio.Enums;
using UnityEngine;

namespace PlayerController.Unity.Physics
{
    public class EffectDriver : MonoBehaviour, IEffectDriver
    {
        [SerializeField] private AudioEffectBridge _audioBridge;
        [SerializeField] private AnimatorEffectBridge _animatorBridge;
        private List<IEffectResult> _queuedEffects = new List<IEffectResult>();

        public void EnqueueEffectResults(List<IEffectResult> effectResults)
        {
            if (effectResults == null) return;
            _queuedEffects.AddRange(effectResults);
        }

        private void Update()
        {
            // Apply the effects
            var effectResults = _queuedEffects;

            // Do your stuff
            foreach (var effect in effectResults)
            {
                HandleAudio(effect);
                HandleVisual(effect);
            }


            // Clear the list
            _queuedEffects.Clear();
        }

        private void HandleAudio(IEffectResult audioEffect)
        {
            // if (audioEffect.Target != EffectTarget.Sound)
            // {
            //     Debug.LogWarning($"Passed effect with target: {audioEffect.Target} to audio handler.");
            //     return;
            // }

            switch (audioEffect)
            {
                case BarkEffect bark:
                    {
                        // Debug.Log("Playing bark");
                        _audioBridge.PlayBark();
                        break;
                    }
                case HowlEffect howl:
                    {
                        // Debug.Log("Playing howl");
                        _audioBridge.PlayHowl();
                        break;
                    }
                case JumpEffect jump:
                    {
                        // Debug.Log("Playing jump");
                        _audioBridge.PlayJump(jump);
                        break;
                    }
                case StepEffect step:
                    {
                        if (step.Approved)
                        {
                            if (step.SoundKey == PlayerSoundKey.Run)
                            {
                                _audioBridge.PlayRun(step);
                            }
                            else
                            {
                                _audioBridge.PlayWalk(step);
                            }
                        }
                        break;
                    }
                case LandEffect landing:
                    {
                        // Debug.Log("Playing landing");
                        _audioBridge.PlayLanding(landing);
                        break;
                    }
            }
        }

        private void HandleVisual(IEffectResult visualEffect)
        {
            // if (visualEffect.Target != EffectTarget.Sound)
            // {
            //     Debug.LogWarning($"Passed effect with target: {visualEffect.Target} to visual handler.");
            //     return;
            // }
            // Debug.Log($"Got visual effect: {visualEffect}");
            _animatorBridge.ApplyEffect(visualEffect);
        }

    }
}