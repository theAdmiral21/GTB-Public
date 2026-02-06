using UnityEngine;
using Primitives.Audio.Abstractions;
using Game.Unity.Audio.DataStructures;

namespace Game.Unity.Audio
{
    //NOTE Make this into a base class with shared update, start, stop, stop all, and PlaySFX methods
    public class MenuAudioPlayer : BaseSoundPlayer, IMenuAudioPlayer
    {
        [SerializeField] private MenuSoundSet _menuSounds;

        public void PlayBackSound(float volume = 1, bool loop = false)
        {
            AudioClip clip = _menuSounds.BackSound;
            PlaySFX(clip, volume, loop);
        }
        public void PlayCancelSound(float volume = 1, bool loop = false)
        {
            AudioClip clip = _menuSounds.CancelSound;
            PlaySFX(clip, volume, loop);
        }

        public void PlaySelectSound(float volume = 1, bool loop = false)
        {
            AudioClip clip = _menuSounds.SelectSound;
            PlaySFX(clip, volume, loop);
        }

        public void PlaySubmitSound(float volume = 1, bool loop = false)
        {
            AudioClip clip = _menuSounds.SubmitSound;
            PlaySFX(clip, volume, loop);
        }

    }
}