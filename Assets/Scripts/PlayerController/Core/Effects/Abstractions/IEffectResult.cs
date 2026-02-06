using System;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.Abstractions
{
    public interface IEffectResult
    {
        public bool Approved { get; }
        public Type EffectType { get; }
        public PlayerSoundKey SoundKey { get; }
    }
}