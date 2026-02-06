using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Menu Sound Set")]
    public class MenuSoundSet : ScriptableObject
    {
        public AudioClip SelectSound;
        public AudioClip SubmitSound;
        public AudioClip BackSound;
        public AudioClip CancelSound;

    }
}