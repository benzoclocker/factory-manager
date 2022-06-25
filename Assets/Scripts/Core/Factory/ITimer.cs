namespace Core.Factory
{
    public interface ITimer : ITickable
    {
        float Delay { get; }
        void ResetDelay();
    }
}