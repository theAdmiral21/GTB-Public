namespace PlayerController.Core.Stats
{
    public struct PlayerThresholds
    {
        public Stat fallThresh;
        public Stat jumpThresh;
        public Stat maxFallSpeed;
        public Stat maxRunSpeed;
        // public PlayerThresholds(PlayerMovementSO stats)
        // {
        //     fallThresh = new Stat(stats.FallThresh);
        //     jumpThresh = new Stat(stats.JumpThresh);
        //     maxFallSpeed = new Stat(stats.FallSpeedLimit);
        //     maxRunSpeed = new Stat(stats.RunSpeed);
        // }
    }
}