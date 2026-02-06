using UnityEngine;

namespace PlayerController.Features.Audio
{
    /// <summary>
    /// Feature service for selecting a random number given a count.
    /// </summary>
    public static class PlayRandomClip
    {
        /// <summary>
        /// Returns a random int value given a count.
        /// </summary>
        /// <param name="count">int</param>
        /// <returns></returns>
        public static int SelectRandom(int count)
        {
            if (count < 0) return -1;
            return Random.Range(0, count);
        }
    }
}