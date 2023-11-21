using System;
using ForestLevelMapMaker.Scripts;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services.Inform;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure.States
{
    public class LoadLevelMapState : IPayloadedState<string>
    {
        private const int SkipIndex = 1;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IInformProgressReaderService _informProgressReaderService;

        public LoadLevelMapState(GameStateMachine gameStateMachine,SceneLoader sceneLoader, IGameFactory gameFactory,
            IInformProgressReaderService informProgressReaderService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _informProgressReaderService = informProgressReaderService;
        }

        public void Exit()
        {
        }

        public void Enter(string nameScene)
        {
            _sceneLoader.Load(nameScene,OnLoaded);
        }

        private void OnLoaded()
        {
            ILevelMap levelMap = _gameFactory.CreateLevelMap();
            levelMap.Selected += OnSelected;
            _informProgressReaderService.Inform();
        }

        private void OnSelected(int levelIndex)
        {
            var sceneName = GetSceneNameByIndex(levelIndex);
            _gameStateMachine.Enter<LoadLevelState, string>(sceneName);
        }

        private static string GetSceneNameByIndex(int levelIndex)
        {
            if (levelIndex + SkipIndex > SceneManager.sceneCountInBuildSettings - SkipIndex)
                throw new IndexOutOfRangeException();

            string scenePath = SceneUtility.GetScenePathByBuildIndex(levelIndex + SkipIndex);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            return sceneName;
        }
    }
}