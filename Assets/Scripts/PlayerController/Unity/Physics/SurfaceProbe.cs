using PlayerController.Application.Physics.DataStructures;
using PlayerController.Features.Physics.DataStructures;
using UnityEngine;

namespace PlayerController.Unity.Physics
{
    /// <summary>
    /// Simple helper class for performing raycasts
    /// </summary>
    public class SurfaceProbe
    {
        public int RaycastCountVertical { get; set; }
        // public int RaycastCountHorizontal { get; set; }
        public CollisionInfo CollisionData;
        public const float SKIN_WIDTH = 0.04f; private Collider2D _collider;
        private RaycastOrigins _rayCastOrigins;
        private Vector2 _raySpacingX;
        private Vector2 _raySpacingY;
        private LayerMask _collisionMask;
        public SurfaceProbe(Collider2D collider)
        {
            _collider = collider;
            RaycastCountVertical = RaycastCountVertical > 5 ? RaycastCountVertical : 5;
            // RaycastCountHorizontal = RaycastCountHorizontal > 5 ? RaycastCountHorizontal : 5;
            _collisionMask = LayerMask.GetMask("Collision");

            _rayCastOrigins = new RaycastOrigins(_collider);
            CalculateSpacing();

            UpdateRaycastOrigins();
        }

        /// <summary>
        /// Method for casting rays downward along the length of the Raycaster's provided collider. This cast returns the first hit that it gets.
        /// </summary>
        /// <param name="dist"></param>
        /// <returns></returns>
        public Collider2D CastDown(float dist)
        {
            UpdateRaycastOrigins();
            for (int i = 0; i < RaycastCountVertical; i++)
            {
                Vector2 origin = _rayCastOrigins.BottomLeft + (_raySpacingX * i);
                Vector2 dir = Vector2.down;
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, dist, _collisionMask);
                // Debugging
                // Debug.Log($"Got hit: {hit.collider.name}");
                Debug.DrawRay(origin, dir * dist, Color.pink);
                // Debug.Log($"GroundProbe, origin: {origin}, vector: {dir * dist}");
                if (hit)
                {
                    return hit.collider;
                }
            }
            return null;
        }

        public Collider2D CastUp(float dist)
        {
            UpdateRaycastOrigins();
            for (int i = 0; i < RaycastCountVertical; i++)
            {
                Vector2 origin = _rayCastOrigins.TopLeft + (_raySpacingX * i);
                Vector2 dir = Vector2.up;
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, dist, _collisionMask);
                // Debugging
                // Debug.Log($"Got hit: {hit.collider.name}");
                Debug.DrawRay(origin, dir * dist, Color.pink);
                // Debug.Log($"GroundProbe, origin: {origin}, vector: {dir * dist}");
                if (hit)
                {
                    return hit.collider;
                }
            }
            return null;
        }

        public Collider2D CastLeft(float dist)
        {
            UpdateRaycastOrigins();
            for (int i = 0; i < RaycastCountVertical; i++)
            {
                Vector2 origin = _rayCastOrigins.BottomLeft + (_raySpacingY * i);
                Vector2 dir = Vector2.left;
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, dist, _collisionMask);
                // Debugging
                // Debug.Log($"Got hit: {hit.collider.name}");
                Debug.DrawRay(origin, dir * dist, Color.pink);
                // Debug.Log($"GroundProbe, origin: {origin}, vector: {dir * dist}");
                if (hit)
                {
                    return hit.collider;
                }
            }
            return null;
        }

        public Collider2D CastRight(float dist)
        {
            UpdateRaycastOrigins();
            for (int i = 0; i < RaycastCountVertical; i++)
            {
                Vector2 origin = _rayCastOrigins.BottomRight + (_raySpacingY * i);
                Vector2 dir = Vector2.right;
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, dist, _collisionMask);
                // Debugging
                // Debug.Log($"Got hit: {hit.collider.name}");
                Debug.DrawRay(origin, dir * dist, Color.pink);
                // Debug.Log($"GroundProbe, origin: {origin}, vector: {dir * dist}");
                if (hit)
                {
                    return hit.collider;
                }
            }
            return null;
        }

        public void ResetCollisions()
        {
            CollisionData.Reset();
        }

        public void SetCollisionMask(LayerMask mask)
        {
            _collisionMask = mask;
        }

        public void CalculateSpacing()
        {
            Bounds bounds = _collider.bounds;
            bounds.Expand(SKIN_WIDTH * -2);

            float xSpacing = bounds.size.x / (RaycastCountVertical - 1);
            // float ySpacing = bounds.size.y / (RaycastCountHorizontal - 1);

            _raySpacingX = new Vector2(xSpacing, 0);
            // _raySpacingY = new Vector2(0, ySpacing);
        }

        public void UpdateRaycastOrigins()
        {
            Bounds bounds = _collider.bounds;
            bounds.Expand(SKIN_WIDTH * -2);

            _rayCastOrigins.TopLeft = new Vector2(bounds.min.x, bounds.max.y);
            _rayCastOrigins.TopRight = new Vector2(bounds.max.x, bounds.max.y);
            _rayCastOrigins.BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            _rayCastOrigins.BottomRight = new Vector2(bounds.max.x, bounds.min.y);
            // DrawCollider(_rayCastOrigins);
        }
    }
}