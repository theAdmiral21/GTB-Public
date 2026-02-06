using System;
using System.Collections.Generic;
using PlayerController.Application.Movement.DataStructures;
using PlayerController.Core.Effects.Abstractions;
using PlayerController.Core.Inputs.DataStructures;
using PlayerController.Core.Movement.Abstractions;
using PlayerController.Core.Movement.DataStructures;
using PlayerController.Core.State.DataStructures;

namespace PlayerController.Application.Abstractions
{
    public interface IEffectDriver
    {
        public void EnqueueEffectResults(List<IEffectResult> effectResults);
    }
}