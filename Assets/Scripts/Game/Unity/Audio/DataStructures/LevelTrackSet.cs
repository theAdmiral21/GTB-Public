using System.Collections.Generic;
using UnityEngine;

namespace Game.Unity.Audio.DataStructures
{

    [CreateAssetMenu(fileName = "LevelMusicHolder", menuName = "Audio/LevelMusicHolder")]
    public class LevelTrackSet : ScriptableObject
    {
        [SerializeField] public List<Song> trackList = new List<Song>();
        private List<string> _trackNames;
        public List<string> TrackNames
        {
            get
            {
                return _trackNames;
            }
            private set
            {
                for (int i = 0; i < trackList.Count; i++)
                {
                    _trackNames[i] = trackList[i].TrackName;
                }
            }
        }

        public float GetSongDuration(int index)
        {
            return trackList[index].SongFile.length;
        }
        public float GetSongDuration(string name)
        {
            int index = GetSongIndex(name);
            return trackList[index].SongFile.length;
        }

        // Get the song from its index
        public Song GetSongFromIndex(int index)
        {
            return trackList[index];
        }
        // Get the song from the string name
        public Song GetSongFromName(string name)
        {
            foreach (Song song in trackList)
            {
                if (song.TrackName.Equals(name))
                {
                    return song;
                }
            }
            return null;
        }

        // Get the song from the string name
        public int GetSongIndex(string name)
        {
            for (int i = 0; i < trackList.Count; i++)
            {
                if (trackList[i].TrackName == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetSongIndex(Song song)
        {
            for (int i = 0; i < trackList.Count; i++)
            {
                if (trackList[i] == song)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}