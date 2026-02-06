using UnityEngine;
using PlayerController.Unity.Movement;
using PlayerController.Application.Abstractions;

namespace PlayerController.Unity
{
    public class MovementController : MonoBehaviour, IMovePlayer
    {
        public Transform PlayerTransform => _playerTransform;
        private Transform _playerTransform;

        private void Awake()
        {
            _playerTransform = GetComponent<Transform>();
        }

        public void Move(Vector2 velocity)
        {
            // Debug.Log($"Moving with velocity: {velocity}");
            _playerTransform.Translate(velocity);
        }
    }
}