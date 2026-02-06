using PlayerController.Application.Abstractions;
using PlayerController.Application.Physics.DataStructures;
using PlayerController.Features.Physics.DataStructures;
using UnityEngine;

namespace PlayerController.Unity.Movement
{
    [RequireComponent(typeof(Collider2D))]
    public class RaycastController : MonoBehaviour, IRaycastController
    {
        public bool DrawRaycast;
        public bool PrintCollisions;
        public CollisionInfo CollisionInfo;
        public int RaycastCountVertical { get; set; }
        public int RaycastCountHorizontal { get; set; }
        protected LayerMask _collisionMask;
        public RaycastOrigins RaycastOrigins => _rayCastOrigins;
        protected RaycastOrigins _rayCastOrigins;
        public const float SKIN_WIDTH = 0.04f;
        public Collider2D Collider => _collider;
        protected Collider2D _collider;
        protected float _rayCastLengthX;
        protected float _rayCastLengthY;
        protected Vector2 _raySpacingX;
        protected Vector2 _raySpacingY;
        protected float _wallJumpCheckDist = 0.1f; // pixels I think
        public virtual void Awake()
        {
            _collider = _collider == null ? GetComponent<Collider2D>() : _collider;
            RaycastCountVertical = RaycastCountVertical > 5 ? RaycastCountVertical : 5;
            RaycastCountHorizontal = RaycastCountHorizontal > 5 ? RaycastCountHorizontal : 5;
            _collisionMask = LayerMask.GetMask("Collision");

            // if (RaycastCountVertical <= 0) RaycastCountVertical = 3;
            _rayCastOrigins = new RaycastOrigins(_collider);
            CalculateSpacing();

            UpdateRaycastOrigins();
        }

        public void ResetCollisions()
        {
            CollisionInfo.Reset();
        }

        public void SetCollisionMask(LayerMask mask)
        {
            _collisionMask = mask;
        }

        public void VerticalRaycast(ref Vector2 velocity)
        {
            // Calculate the length of the ray
            _rayCastLengthY = Mathf.Abs(velocity.y) + SKIN_WIDTH;
            for (int i = 0; i < RaycastCountVertical; i++)
            {
                Vector2 origin = velocity.y <= 0 ? _rayCastOrigins.BottomLeft + (_raySpacingX * i) : _rayCastOrigins.TopLeft + (_raySpacingX * i);
                Vector2 dir = velocity.y <= 0 ? Vector2.down : Vector2.up;
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, _rayCastLengthY, _collisionMask);
                if (hit)
                {
                    velocity.y = (hit.distance - SKIN_WIDTH) * dir.y;
                    _rayCastLengthY = hit.distance;
                    // Update collision info
                    CollisionInfo.Below = dir.y == -1;
                    CollisionInfo.Above = dir.y == 1;
                }

                if (DrawRaycast)
                {
                    Debug.DrawRay(origin, dir * _rayCastLengthY, Color.green);
                }
            }
        }

        public void HorizontalRaycast(ref Vector2 velocity)
        {
            // Calculate the length of the ray
            _rayCastLengthX = Mathf.Abs(velocity.x) + SKIN_WIDTH;

            for (int i = 0; i < RaycastCountHorizontal; i++)
            {
                Vector2 origin = velocity.x < 0 ? _rayCastOrigins.BottomLeft + (_raySpacingY * i) : _rayCastOrigins.BottomRight + (_raySpacingY * i);
                Vector2 dir = velocity.x < 0 ? Vector2.left : Vector2.right;
                float castDist = _rayCastLengthX;
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, castDist, _collisionMask);
                if (hit)
                {

                    velocity.x = (hit.distance - SKIN_WIDTH) * dir.x;
                    _rayCastLengthX = hit.distance;
                    // Update collision info
                    CollisionInfo.Left = dir.x == -1;
                    CollisionInfo.Right = dir.x == 1;
                }
                if (DrawRaycast)
                {
                    Debug.DrawRay(origin, dir * _rayCastLengthX, Color.green);
                }
            }
        }

        public void CornerRayCast(ref Vector2 velocity)
        {
            // You could probably cache these to speed up calcs
            // NOTE You treat everything as positive to make adding or subtracting SKIN_WIDTH easier later
            _rayCastLengthX = Mathf.Abs(velocity.x) + SKIN_WIDTH;
            float dirX = Mathf.Sign(velocity.x);
            _rayCastLengthY = Mathf.Abs(velocity.y) + SKIN_WIDTH;
            float dirY = Mathf.Sign(velocity.y);

            // NOTE You don't need a for loop here because you only need to check one of the corners
            Vector2 origin = Vector2.zero;
            if (dirX < 0 && dirY < 0)
            {
                origin = _rayCastOrigins.BottomLeft;
            }
            if (dirX < 0 && dirY > 0)
            {
                origin = _rayCastOrigins.TopLeft;
            }
            if (dirX > 0 && dirY > 0)
            {
                origin = _rayCastOrigins.TopRight;
            }
            if (dirX > 0 && dirY < 0)
            {
                origin = _rayCastOrigins.BottomRight;
            }
            if (origin == Vector2.zero)
            {
                Debug.LogError("[RaycastController] Could not determine origin for corner cast.");
                return;
            }

            // Check for a collision
            RaycastHit2D hit = Physics2D.Raycast(origin, velocity.normalized, velocity.magnitude, _collisionMask);
            if (hit)
            {
                // Debug.Log($"Corner collision: {hit.collider.name}");
                velocity.x = hit.distance * velocity.normalized.x - SKIN_WIDTH;
                velocity.y = hit.distance * velocity.normalized.y - SKIN_WIDTH;
            }
            Debug.DrawRay(origin, velocity, Color.white);
        }

        public bool WallJumpCheck()
        {
            // What should the check distance be? Should it be constant? I think it should be constant because you may not always be moving in X when trying to initiate a wall jump
            if (!CollisionInfo.Below)
            {
                for (int i = 0; i < RaycastCountHorizontal; i++)
                {
                    Vector2 origin = transform.localScale.x < 0 ? _rayCastOrigins.BottomLeft + (_raySpacingY * i) : _rayCastOrigins.BottomRight + (_raySpacingY * i);
                    Vector2 dir = transform.localScale.x < 0 ? Vector2.left : Vector2.right;
                    float rayLength = _wallJumpCheckDist + SKIN_WIDTH;
                    RaycastHit2D hit = Physics2D.Raycast(origin, dir, rayLength, _collisionMask);
                    // Check that you hit a wall and not the floor
                    if (hit)
                    {
                        // Debug.Log($"Contact point normal: {hit.normal}");
                        Debug.DrawRay(origin, dir * rayLength, Color.yellow);
                        return true;
                    }

                    Debug.DrawRay(origin, dir * rayLength, Color.yellow);
                }
            }
            return false;
        }

        public void CalculateSpacing()
        {
            Bounds bounds = _collider.bounds;
            bounds.Expand(SKIN_WIDTH * -2);

            float xSpacing = bounds.size.x / (RaycastCountVertical - 1);
            float ySpacing = bounds.size.y / (RaycastCountHorizontal - 1);

            _raySpacingX = new Vector2(xSpacing, 0);
            _raySpacingY = new Vector2(0, ySpacing);
        }

        public void UpdateRaycastOrigins()
        {
            Bounds bounds = _collider.bounds;
            bounds.Expand(SKIN_WIDTH * -2);

            _rayCastOrigins.TopLeft = new Vector2(bounds.min.x, bounds.max.y);
            _rayCastOrigins.TopRight = new Vector2(bounds.max.x, bounds.max.y);
            _rayCastOrigins.BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            _rayCastOrigins.BottomRight = new Vector2(bounds.max.x, bounds.min.y);
            DrawCollider(_rayCastOrigins);
        }

        private void DrawCollider(RaycastOrigins raycastOrigins)
        {
            Debug.DrawLine(raycastOrigins.BottomLeft, raycastOrigins.TopLeft, Color.green);
            Debug.DrawLine(raycastOrigins.BottomLeft, raycastOrigins.BottomRight, Color.green);
            Debug.DrawLine(raycastOrigins.TopRight, raycastOrigins.TopLeft, Color.green);
            Debug.DrawLine(raycastOrigins.BottomRight, raycastOrigins.TopRight, Color.green);
        }
    }
}