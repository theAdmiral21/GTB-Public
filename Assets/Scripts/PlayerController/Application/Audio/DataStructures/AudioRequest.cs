namespace PlayerController.Application.Audio.DataStructures
{
    public readonly struct AudioRequest
    {
        public readonly bool Requested;
        public AudioRequest(bool requested)
        {
            Requested = requested;
        }
    }
}