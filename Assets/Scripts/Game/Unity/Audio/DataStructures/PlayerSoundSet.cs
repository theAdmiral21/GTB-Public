using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(menuName = "Game/Audio/Player Sound Set")]
    public class PlayerSoundSet : ScriptableObject
    {
        public AudioClip[] Barks;
        public AudioClip[] Howls;

        public float BarkVolume = 1f;
        public Vector2 BarkPitchRange = new Vector2(0.95f, 1.05f);

        public AudioClip GetRandomBark()
        {
            int randVal = Random.Range(0, Barks.Length);
            return Barks[randVal];
        }

        public AudioClip GetRandomHowl()
        {
            int randVal = Random.Range(0, Howls.Length);
            return Howls[randVal];
        }
    }
}