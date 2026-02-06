using System.Collections.Generic;
using PlayerController.Application.Effects;
using PlayerController.Application.Effects.Abstractions;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Effects.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Core.Stats;
using PlayerController.Unity.Physics;
using UnityEngine;

namespace PlayerController.Unity.Effects
{
    public class PlayerEffectOrchestrator : MonoBehaviour, IPlayerEffectOrchestrator
    {
        [SerializeField] private EffectDriver _effectDriver;
        private List<IEffectResult> _effectResults = new List<IEffectResult>();
        private PhysicsContext _physicsContext;
        private StepDetector _stepDetector;
        private PlayerStats _stats;

        public void SetPhysicsContext(PhysicsContext physicsContext)
        {
            _physicsContext = physicsContext;
        }

        public void SetPlayerStats(PlayerStats stats)
        {
            _stats = stats;
            _stepDetector = new StepDetector(_stats);
        }

        public void BuildEffectQueue(List<IActionResult> actionResults)
        {
            foreach (IActionResult actionResult in actionResults)
            {
                if (!actionResult.Approved) continue;

                // Map the approved actions to their effects
                switch (actionResult)
                {
                    case JumpResult jump:
                        {
                            _effectResults.Add(new JumpEffect(true, _physicsContext.Surface));
                            break;
                        }
                    case JumpCancelResult jumpCancel:
                        {
                            break;
                        }
                    case FallResult fall:
                        {
                            break;
                        }
                    case RunResult run:
                        {
                            StepEffect effect = _stepDetector.TryStep(_physicsContext, 5f);
                            _effectResults.Add(effect);
                            break;
                        }
                    case RunStopResult runStop:
                        {

                        }
                        break;

                    case QuickStepResult quickStep:
                        {
                            break;
                        }
                    case BarkResult bark:
                        {
                            // Debug.Log("Got bark effect");
                            _effectResults.Add(new BarkEffect(true));
                            break;
                        }
                    case LandingResult land:
                        {
                            // Debug.Log($"Got landing effect");
                            _effectResults.Add(new LandEffect(true, land.Surface));
                            break;
                        }
                }
            }
            _effectDriver.EnqueueEffectResults(_effectResults);
            _effectResults.Clear();
        }


    }
}