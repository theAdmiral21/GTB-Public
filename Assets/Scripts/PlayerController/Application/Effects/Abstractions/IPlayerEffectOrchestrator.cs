using System.Collections.Generic;
using PlayerController.Core.Movement.Abstractions;

namespace PlayerController.Application.Effects.Abstractions
{
    public interface IPlayerEffectOrchestrator
    {
        public void BuildEffectQueue(List<IActionResult> effect);
    }
}