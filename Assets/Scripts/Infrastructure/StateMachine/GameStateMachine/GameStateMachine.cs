using System;
using System.Collections.Generic;
using Infrastructure.Container;
using Infrastructure.StateMachine.GameStateMachine.States;
using Services.Factory.GameFactory;
using Services.Factory.UIFactory;
using Services.SceneLoader;

namespace Infrastructure.StateMachine.GameStateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public GameStateMachine(ServiceContainer serviceContainer)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, serviceContainer),
                [typeof(LoadLevelState)] = new LoadLevelState(this,
                    serviceContainer.Single<ISceneLoader>(),
                    serviceContainer.Single<IGameFactory>(),
                    serviceContainer.Single<IUIFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this)
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();
            _activeState = _states[typeof(TState)];
            _activeState.Enter();
        }
    }
}