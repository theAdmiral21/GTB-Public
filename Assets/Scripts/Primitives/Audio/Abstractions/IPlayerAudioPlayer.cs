using Primitives.Audio.Enums;
using UnityEngine;

namespace Primitives.Audio.Abstractions
{
    public interface IPlayerAudioPlayer
    {
        public void PlayBarkSound(float volume = 1f, bool loop = false);
        public void PlayHowlSound(float volume = 1f, bool loop = false);
        public void PlayWalkSound(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayRunSound(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayJumpSound(SurfaceType surface, float volume = 1f, bool loop = false);
        public void PlayLandingSound(SurfaceType surface, float volume = 1f, bool loop = false);

    }
}