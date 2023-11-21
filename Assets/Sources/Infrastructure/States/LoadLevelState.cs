using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services.Inform;
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
        private readonly IInformProgressReaderService _informProgressReaderService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService,
            IInformProgressReaderService informProgressReaderService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _informProgressReaderService = informProgressReaderService;
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
            _informProgressReaderService.Inform();
        }
    }
}