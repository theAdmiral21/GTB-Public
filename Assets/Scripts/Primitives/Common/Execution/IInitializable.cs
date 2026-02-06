namespace Primitives.Execution
{
    public interface IInitializable<T>
    {
        public int Priority { get; }
        public void Initialize(T context);

        public void PostInitialize(T context);
    }
}