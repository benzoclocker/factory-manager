namespace Infrastructure.Container
{
    public class ServiceContainer
    {
        private static ServiceContainer _instance;

        public static ServiceContainer Container => _instance ?? new ServiceContainer();

        public void RegisterSingle<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        public TService Single<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        private class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}