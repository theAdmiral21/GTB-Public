
using System.Collections.Generic;
using UnityEngine;

namespace Primitives.Audio.Abstractions
{
    public abstract class BaseSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSourcePreFab;
        [SerializeField] private int queueSize = 10;
        private Queue<AudioSource> audioSources = new();
        private List<AudioSource> activeSources = new();

        private void Start()
        {
            // Fill the queue
            for (int i = 0; i < queueSize; i++)
            {
                var source = Instantiate(audioSourcePreFab, transform);
                source.playOnAwake = false;
                audioSources.Enqueue(source);
            }
        }

        private void Update()
        {
            // Recycle finished sources
            for (int i = activeSources.Count - 1; i >= 0; i--)
            {
                if (!activeSources[i].isPlaying)
                {
                    audioSources.Enqueue(activeSources[i]);
                    activeSources[i].gameObject.SetActive(false);
                    activeSources.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Method for playing a sound
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="loop"></param>
        /// <returns>AudioSource</returns>
        public AudioSource PlaySFX(AudioClip clip, float volume = 1f, bool loop = false)
        {
            if (clip == null || audioSources.Count == 0) return null;
            // Get the audio source from the queue
            var source = audioSources.Dequeue();
            // Set up the audio source
            source.clip = clip;
            source.volume = volume;
            source.loop = loop;
            source.gameObject.SetActive(true);
            // Debug.Log($"Playing: {clip.name}");
            source.Play();

            // If the audio source isn't being looped, save it for later
            if (!loop)
            {
                activeSources.Add(source);
            }
            return source;
        }

        /// <summary>
        /// Method for stopping a sound
        /// </summary>
        /// <param name="source"></param>
        public void StopSFX(AudioSource source)
        {
            if (source == null) return;
            // Stop the clip and reset the audio source's settings
            ResetSoundSettings(source);

            // if the audio source isn't in the queue, add it
            if (!audioSources.Contains(source))
            {
                audioSources.Enqueue(source);
            }
            // remove the source from the active sources list
            activeSources.Remove(source);
        }

        /// <summary>
        /// Stops all playing sounds.
        /// </summary>
        public void StopAll()
        {
            // Find all active sources and stop them
            foreach (AudioSource source in activeSources)
            {
                // Stop the clip and reset the audio source's settings
                ResetSoundSettings(source);
            }
        }

        private void ResetSoundSettings(AudioSource source)
        {
            // Stop the clip and reset the audio source's settings
            source.Stop();
            source.loop = false;
            source.clip = null;
            source.gameObject.SetActive(false);
            source.gameObject.transform.position = gameObject.transform.position;
        }
    }
}