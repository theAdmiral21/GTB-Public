using System.Collections.Generic;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{
    [CreateAssetMenu(fileName = "Song", menuName = "Audio/Song")]
    public class Song : ScriptableObject
    {
        [SerializeField] public string TrackName;
        [SerializeField] public AudioClip SongFile;
        [SerializeField] public float PlaybackSpeed = 1;
        [SerializeField] public float LoopPoint;


        public float GetLoopPoint()
        {
            return LoopPoint;
        }
    }
}