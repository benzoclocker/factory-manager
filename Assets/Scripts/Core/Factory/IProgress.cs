namespace Core.Factory
{
    public interface IProgress : ITickable
    {
        bool IsActive { get; }
        void DeactivateProgress();
        void ActivateProgress();
    }
}