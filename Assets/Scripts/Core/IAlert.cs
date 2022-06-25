using Core.Reasons;

namespace Core
{
    public interface IAlert
    {
        void AlertPlayer(Factory.Factory factory, IReason reason);
    }
}