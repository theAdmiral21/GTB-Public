using System;
using PlayerController.Core.Effects.Abstractions;
using Primitives.Audio;
using Primitives.Audio.Enums;

namespace PlayerController.Core.Effects.DataStructures
{
    public struct HowlEffect : IEffectResult
    {
        public bool Approved => _approved;
        private bool _approved;


        public Type EffectType => typeof(HowlEffect);

        public PlayerSoundKey SoundKey => PlayerSoundKey.Howl;

        public HowlEffect(bool approved)
        {
            _approved = approved;
        }
    }
}