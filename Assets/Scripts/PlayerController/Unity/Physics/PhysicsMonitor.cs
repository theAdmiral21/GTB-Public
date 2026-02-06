using PlayerController.Core.Physics.DataStructures;
using PlayerController.Core.State.DataStructures;
using PlayerController.Unity.Effects;
using PlayerController.Unity.Movement;
using Primitives.Audio;
using Primitives.Audio.Enums;
using UnityEngine;

namespace PlayerController.Unity.Physics
{

    [RequireComponent(typeof(Collider2D))]
    public class PhysicsMonitor : MonoBehaviour
    {
        [SerializeField] private RaycastController _raycastController;
        [SerializeField] private float FallThresh;
        [SerializeField] private float RiseThresh;

        public WallContact WallContactSide { get; private set; }
        public string ContactTag { get; private set; }
        public PhysicsContext CurrentContext { get; private set; }

        const float MIN_GROUND_CHECK = 0.1f;
        const float MIN_WALL_CHECK = 0.1f;

        public float TerrainAngle => _terrainAngle;
        private float _terrainAngle;

        private Collider2D playerCollider;
        private SurfaceProbe _surfaceProbe;
        private Vector2 _velocity;
        private Vector3 _prevPosition;

        private void Awake()
        {
            playerCollider = GetComponent<BoxCollider2D>();
            _surfaceProbe = new SurfaceProbe(playerCollider);

        }

        public void ObserveVelocity(Vector2 velocity)
        {
            _velocity = velocity;
            // Debug.Log($"Updated velocity: {_velocity}");
        }

        public void GetPhysicsState()
        {
            PhysicsContext context = new PhysicsContext
            {
                IsGrounded = CheckGrounded(),
                IsRising = CheckRising(),
                IsFalling = CheckFalling(),
                IsOnPlatform = CheckPlatformed(),
                IsTouchingWall = CheckTouchingWall(),
                WallContactType = WallContactSide,
                IsWallSliding = CheckWallSliding(),
                IsHanging = false,
                IsFloating = false,
                IsSliding = false,
                FloorContactType = ContactType.NormalSurface,
                Surface = ResolveSurface(),
                DeltaPosition = CalcPositionChange(),
            };
            context.SetVelocity(_velocity);

            CurrentContext = context;

        }

        private void FixedUpdate()
        {
            GetPhysicsState();
        }

        private bool CheckGrounded()
        {
            return GroundedRaycast();
        }

        /// <summary>
        /// Method for verifying if the player is on the ground.
        /// </summary>
        private bool GroundedRaycast()
        {
            Collider2D collider = _surfaceProbe.CastDown(MIN_GROUND_CHECK);
            if (collider != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method for verifying if the player is on a platform.
        /// </summary>
        private bool CheckPlatformed()
        {
            return OnPlatformRaycast();
        }

        private bool OnPlatformRaycast()
        {
            Collider2D collider = _surfaceProbe.CastDown(MIN_GROUND_CHECK);
            if (collider != null && collider.CompareTag("Platform"))
            {
                return true;
            }
            return false;
        }

        private SurfaceType ResolveSurface()
        {
            Collider2D collider = _surfaceProbe.CastDown(MIN_GROUND_CHECK);
            if (collider == null) return SurfaceType.None;
            // Debug.Log($"Surface owner: {collider.name}");

            SurfaceTag surface = collider.GetComponent<SurfaceTag>();

            if (surface == null)
            {
                return SurfaceType.None;
            }
            return surface.Tag;

        }

        private Vector3 CalcPositionChange()
        {
            var deltaPosition = transform.position - _prevPosition;
            _prevPosition = transform.position;
            return deltaPosition;
        }

        /// <summary>
        /// Method for verifying if the player is wallsliding.
        /// </summary>
        private bool CheckTouchingWall()
        {


            if (_surfaceProbe.CastRight(MIN_WALL_CHECK))
            {
                WallContactSide = WallContact.Right;
                return true;
            }
            else if (_surfaceProbe.CastLeft(MIN_WALL_CHECK))
            {
                WallContactSide = WallContact.Left;
                return true;
            }
            else
            {
                WallContactSide = WallContact.None;
                return false;
            }

        }


        private bool CheckWallSliding()
        {
            return CheckFalling() && CheckTouchingWall();
        }

        /// <summary>
        /// Method for verifying if the player is falling.
        /// </summary>
        private bool CheckFalling()
        {
            if (!CheckGrounded() && !CheckPlatformed())
            {
                if (_velocity.y <= FallThresh)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method for verifying if the player is hanging from something.
        /// </summary>
        // private bool CheckHanging()
        // {
        //     if (playerFSM.IsGrabbing)
        //     {
        //         return true;
        //     }
        //     return false;
        // }

        /// <summary>
        /// Method for verifying if the player is rising through the air.
        /// </summary>
        private bool CheckRising()
        {
            if (!CheckGrounded() && !CheckPlatformed())
            {
                if (_velocity.y > RiseThresh)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method for verifying if the player is floating.
        /// </summary>
        private bool CheckFloating()
        {
            if (!CheckGrounded() && !CheckRising() && !CheckFalling())
            {
                return true;
            }
            return false;
        }

        private bool CheckSliding()
        {
            if (ContactTag == "Slide" && _velocity.y < 0)
            {
                return true;
            }
            return false;
        }

        // public float GetTerrainAngle()
        // {
        //     // ray cast straight down
        //     float dist = 3f;
        //     RaycastHit2D hit = Physics2D.Raycast(_rBody.position, Vector2.down, dist);
        //     if (hit.collider != null)
        //     {
        //         _terrainAngle = Mathf.Atan2(hit.normal.y, hit.normal.x);
        //         if (_terrainAngle == 90f || _terrainAngle == -90f)
        //         {
        //             GroundType = GroundType.Flat;
        //             return TerrainAngle;
        //         }
        //         GroundType = GroundType.Slope;
        //         return TerrainAngle;
        //     }
        //     GroundType = GroundType.None;
        //     return -Mathf.Infinity;

        // }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider != null)
            {
                if (ContactTag != collision.collider.tag)
                {
                    ContactTag = collision.collider.tag;
                }
            }
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.collider != null)
            {
                if (ContactTag != collision.collider.tag)
                {
                    ContactTag = collision.collider.tag;
                }
            }
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider != null)
            {
                if (ContactTag != collision.collider.tag)
                {
                    ContactTag = collision.collider.tag;
                }
            }
        }
    }
}
