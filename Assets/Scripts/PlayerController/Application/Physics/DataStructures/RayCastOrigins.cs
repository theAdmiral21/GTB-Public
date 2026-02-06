using UnityEngine;

namespace PlayerController.Features.Physics.DataStructures
{
    public struct RaycastOrigins
    {
        public Vector2 TopLeft;
        public Vector2 TopRight;
        public Vector2 BottomLeft;
        public Vector2 BottomRight;

        public RaycastOrigins(Collider2D collider)
        {
            Bounds bounds = collider.bounds;
            TopLeft = new Vector2(bounds.min.x, bounds.max.y);
            TopRight = new Vector2(bounds.max.x, bounds.max.y);
            BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            BottomRight = new Vector2(bounds.max.x, bounds.min.y);
        }
    }
}