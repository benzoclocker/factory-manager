using Infrastructure.Container;
using Services.Factory.GameFactory;
using Services.Factory.UIFactory;
using Services.Input;
using Services.Providers.AssetProvider;
using Services.Providers.ConfigProvider;
using Services.SceneLoader;

namespace Infrastructure.StateMachine.GameStateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ServiceContainer _container;

        public BootstrapState(IGameStateMachine gameStateMachine, ServiceContainer container)
        {
            _gameStateMachine = gameStateMachine;
            _container = container;

            RegisterServices();
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
            
        }

        private void RegisterServices()
        {
            _container.RegisterSingle<IGameStateMachine>(_gameStateMachine);
            _container.RegisterSingle<ISceneLoader>(new SceneLoader());
            _container.RegisterSingle<IInputService>(new InputService());
            _container.RegisterSingle<IGameConfigProvider>(new GameConfigProvider());
            _container.RegisterSingle<IAssetProvider>(new AssetProvider());
            _container.RegisterSingle<IUIFactory>(new UIFactory
                (_container.Single<IAssetProvider>()));
            _container.RegisterSingle<IGameFactory>
            (new GameFactory
            (_container.Single<IAssetProvider>(), _container.Single<IInputService>(),
                _container.Single<IGameConfigProvider>()));
        }
    }
}