using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic;

namespace Sources.Infrastructure.States
{
    internal class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string nameScene)
        {
            _gameFactory.Cleanup();
            _sceneLoader.Load(nameScene,OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _gameFactory.CreateTargetDestroy();
            InformProgressReader();
        }

        private void InformProgressReader()
        {
            foreach (ISavedProgressReader savedProgressReader in _gameFactory.SavedProgressReaders)
            {
                savedProgressReader.LoadProgress(_progressService.Progress);
            }
        }
    }
}