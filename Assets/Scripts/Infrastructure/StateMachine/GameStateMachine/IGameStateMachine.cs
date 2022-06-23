using Infrastructure.Container;

namespace Infrastructure.StateMachine.GameStateMachine
{
    public interface IGameStateMachine : IService
    {
        void Enter<TState>() where TState : IState;
    }
}