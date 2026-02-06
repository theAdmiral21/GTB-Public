using PlayerController.Core.Effects.DataStructures;
using Primitives.Audio;
using Primitives.Audio.Abstractions;
using UnityEngine;

namespace PlayerController.Unity.Audio
{
    /// <summary>
    /// Helper class for connecting a given sound effect to an action.
    /// </summary>
    public class AudioEffectBridge : MonoBehaviour
    {
        [SerializeField] MonoBehaviour _audioPlayerMono;
        private IPlayerAudioPlayer _audioPlayer;

        private void Awake()
        {
            _audioPlayer = _audioPlayerMono as IPlayerAudioPlayer;
            if (_audioPlayer == null)
            {
                Debug.LogError($"{_audioPlayerMono.name} does not implement IAudioPlayer");
            }
        }
        public void PlayBark()
        {
            _audioPlayer.PlayBarkSound();
        }
        public void PlayHowl()
        {
            _audioPlayer.PlayHowlSound();
        }

        public void PlayJump(JumpEffect jumpEffect)
        {
            _audioPlayer.PlayJumpSound(jumpEffect.Surface);
        }

        public void PlayWalk(StepEffect stepEffect)
        {
            _audioPlayer.PlayWalkSound(stepEffect.Surface);
        }
        public void PlayRun(StepEffect stepEffect)
        {
            _audioPlayer.PlayRunSound(stepEffect.Surface);
        }

        public void PlayLanding(LandEffect landEffect)
        {
            _audioPlayer.PlayLandingSound(landEffect.Surface);
        }
    }
}
