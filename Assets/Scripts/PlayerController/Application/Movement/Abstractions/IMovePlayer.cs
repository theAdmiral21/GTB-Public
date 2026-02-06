using UnityEngine;

namespace PlayerController.Application.Abstractions
{
    public interface IMovePlayer
    {
        public void Move(Vector2 velocity);
    }
}