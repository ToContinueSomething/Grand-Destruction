namespace Sources.Infrastructure.States
{
    public class LoadLevelMapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LevelMapLoader _levelMapLoader;

        public LoadLevelMapState(GameStateMachine gameStateMachine, LevelMapLoader levelMapLoader)
        {
            _gameStateMachine = gameStateMachine;
            _levelMapLoader = levelMapLoader;
        }

        public void Exit()
        {
            
        }

        public void Enter()
        {
            _levelMapLoader.Load(OnLoaded);
        }

        private void OnLoaded(string nameScene)
        {
            _gameStateMachine.Enter<LoadProgressState,string>(nameScene);
        }
    }
}