using Services.Factory.GameFactory;
using Services.Factory.UIFactory;
using Services.SceneLoader;

namespace Infrastructure.StateMachine.GameStateMachine.States
{
    public class LoadLevelState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, 
            IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
        }
        
        public void Enter()
        {
            _sceneLoader.LoadScene("GameScene", CreateGameWorld);
        }

        public void Exit()
        {
           
        }

        private void CreateGameWorld()
        {
            _uiFactory.CreateControllerUI();
            
            _gameFactory.CreateGround();
            _gameFactory.CreatePlayer();
            _gameFactory.CreateCamera();
            _gameFactory.CreateFirstFactory();
            _gameFactory.CreateSecondFactory();
            _gameFactory.CreateThirdFactory();
            
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}