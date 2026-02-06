namespace PlayerController.Features.Abstractions
{
    public interface IInputSubscriber
    {
        public void Subscribe();
        public void Unsubscribe();
    }
}
