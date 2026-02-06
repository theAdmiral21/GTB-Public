using UnityEngine;
using Primitives.Audio.Abstractions;
using Game.Unity.Audio.DataStructures;
using Primitives.Audio.Enums;

namespace Game.Unity.Audio
{
    public class PlayerAudioPlayer : BaseSoundPlayer, IPlayerAudioPlayer
    {
        [SerializeField] private PlayerSoundSet _playerSounds;
        [SerializeField] private FootstepLibrary _surfaceSounds;

        public void PlayBarkSound(float volume = 1f, bool loop = false)
        {
            var clip = _playerSounds.GetRandomBark();
            PlaySFX(clip, volume, loop);
        }
        public void PlayHowlSound(float volume = 1f, bool loop = false)
        {
            var clip = _playerSounds.GetRandomHowl();
            PlaySFX(clip, volume, loop);
        }

        public void PlayWalkSound(SurfaceType surface, float volume = 1f, bool loop = false)
        {
            var clip = _surfaceSounds.GetWalkSoundEffect(surface);
            PlaySFX(clip, volume, loop);
        }
        public void PlayRunSound(SurfaceType surface, float volume = 1f, bool loop = false)
        {
            var clip = _surfaceSounds.GetRunSoundEffect(surface);
            PlaySFX(clip, volume, loop);
        }

        public void PlayJumpSound(SurfaceType surface, float volume = 1f, bool loop = false)
        {
            var clip = _surfaceSounds.GetJumpSoundEffect(surface);
            PlaySFX(clip, volume, loop);
        }

        public void PlayLandingSound(SurfaceType surface, float volume = 1f, bool loop = false)
        {
            var clip = _surfaceSounds.GetLandingSoundEffect(surface);
            PlaySFX(clip, volume, loop);
        }
    }
}