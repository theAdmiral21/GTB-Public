using UnityEngine;

namespace Primitives.Audio.Abstractions
{
    public interface IMenuAudioPlayer
    {
        public void PlaySelectSound(float volume = 1f, bool loop = false);
        public void PlayCancelSound(float volume = 1f, bool loop = false);
        public void PlaySubmitSound(float volume = 1f, bool loop = false);
        public void PlayBackSound(float volume = 1f, bool loop = false);

    }
}