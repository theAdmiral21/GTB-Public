
using UnityEngine;

namespace PlayerController.Core.Stats
{
    public struct PlayerOffsets
    {
        public Vector2 GrabPoint;
        public Vector2 SwingPoint;


        public PlayerOffsets(PlayerPhysicalData data)
        {
            GrabPoint = data.GrabPoint;
            SwingPoint = data.SwingPoint;

        }
    }


}