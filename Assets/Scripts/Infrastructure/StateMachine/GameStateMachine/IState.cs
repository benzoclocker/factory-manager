namespace Infrastructure.StateMachine.GameStateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}