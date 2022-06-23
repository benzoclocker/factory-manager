using Infrastructure.StateMachine.GameStateMachine;

namespace Core
{
    public class Game
    {
        public readonly IGameStateMachine GameStateMachine;

        public Game(IGameStateMachine gameStateMachine)
        {
            GameStateMachine = gameStateMachine;
        }
    }
}