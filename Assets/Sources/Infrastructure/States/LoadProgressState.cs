using Sources.Data;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Infrastructure.Services.SaveLoad;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string NameLevelMapScene = "LevelMap";
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine,IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelMapState,string>(NameLevelMapScene);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? GetNewProgress();
        }

        private PlayerProgress GetNewProgress()
        {
            return new PlayerProgress(SceneManager.sceneCountInBuildSettings - 1);
        }

        public void Exit()
        {
            
        }
    }
}