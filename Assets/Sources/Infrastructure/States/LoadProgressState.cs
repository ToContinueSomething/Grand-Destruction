using Sources.Data;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Sources.Infrastructure.States
{
    public class LoadProgressState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine,IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter(string nameScene)
        {
            LoadProgressOrInitNew();
            _gameStateMachine.Enter<LoadLevelState,string>(nameScene);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? GetNewProgress();
        }

        private PlayerProgress GetNewProgress()
        {
            return new PlayerProgress();
        }

        public void Exit()
        {
            
        }

        
    }
}