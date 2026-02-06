namespace PlayerController.Core.Movement.DataStructures
{
    /// <summary>
    /// Available jump types for the player. This defines the "vocabulary" of Core.
    /// </summary>
    public enum JumpType
    {
        None,
        Ground,
        Coyote,
        Double,
        WallJump
    }
}