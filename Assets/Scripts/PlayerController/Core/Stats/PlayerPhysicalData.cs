using UnityEngine;

namespace PlayerController.Core.Stats
{
    [CreateAssetMenu(fileName = "PlayerPhysicalData", menuName = "Player/PlayerPhysicalData")]

    public class PlayerPhysicalData : ScriptableObject
    {
        // Offsets
        //=========================================================================
        [SerializeField] private Vector2 grabPoint = new Vector2(1f, 1.2f);
        public Vector2 GrabPoint { get => grabPoint; set => grabPoint = value; }
        [SerializeField] private Vector2 swingPoint = new Vector2(0f, 1.8f);
        public Vector2 SwingPoint { get => swingPoint; set => swingPoint = value; }
    }
}