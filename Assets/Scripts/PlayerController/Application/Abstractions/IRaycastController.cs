using UnityEngine;

namespace PlayerController.Application.Abstractions
{
    public interface IRaycastController
    {
        public int RaycastCountVertical { get; set; }
        public int RaycastCountHorizontal { get; set; }
        public void HorizontalRaycast(ref Vector2 velocity);
        public void VerticalRaycast(ref Vector2 velocity);
        public void CornerRayCast(ref Vector2 velocity);
        public void UpdateRaycastOrigins();
        public void CalculateSpacing();
        public void ResetCollisions();
        public bool WallJumpCheck();
    }
}