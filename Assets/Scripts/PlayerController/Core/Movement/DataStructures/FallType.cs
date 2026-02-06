namespace PlayerController.Core.Movement.DataStructures
{
    /// <summary>
    /// Available fall types for the player. This defines the "vocabulary" of Core.
    /// </summary>
    public enum FallType
    {
        None,
        Fast,
        Slow,
        WallSlide,
    }
}