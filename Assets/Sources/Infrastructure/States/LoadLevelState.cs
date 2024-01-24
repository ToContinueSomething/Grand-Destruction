using DG.Tweening;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services.Inform;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic;
using Sources.Logic.StateMachines;
using Sources.Logic.StateMachines.Camera;
using Sources.Logic.UI.Services.Factory;

namespace Sources.Infrastructure.States
{
    internal class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IInformProgressReaderService _informProgressReaderService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IPersistentProgressService progressService,
            IInformProgressReaderService informProgressReaderService,IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _informProgressReaderService = informProgressReaderService;
            _uiFactory = uiFactory;
        }

        public void Enter(string nameScene)
        {
            DOTween.Clear();
            _gameFactory.Cleanup();
            _sceneLoader.Load(nameScene,OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            InitUI();
            var levelStateMachine = InitGameWorld();
            _informProgressReaderService.Inform();
            
            levelStateMachine.EnterStateMachine<CameraStateMachine>();
        }

        private LevelStateMachine InitGameWorld()
        {
            _gameFactory.CreateDestroyObject();
            _gameFactory.CreateAnchor();
            _gameFactory.CreateHud();
            _gameFactory.CreateUI();
            _gameFactory.CreateAICamera();
            _gameFactory.CreateGameFinish();
            LevelStateMachine levelStateMachine = _gameFactory.CreateLevelStateMachine();
            return levelStateMachine;
        }

        private void InitUI()
        {
            _uiFactory.CreateUIRoot();
        }
    }
}