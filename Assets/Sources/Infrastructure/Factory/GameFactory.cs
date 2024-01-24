using System.Collections.Generic;
using ForestLevelMapMaker.Scripts;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Infrastructure.Services.StaticData;
using Sources.Logic;
using Sources.Logic.StateMachines;
using Sources.Logic.StateMachines.Camera;
using Sources.Logic.StateMachines.Target;
using Sources.Logic.UI;
using Sources.Logic.UI.Elements;
using Sources.Logic.UI.Services.Windows;
using Sources.Models;
using Sources.Services.Input;
using Sources.StaticData;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private const string TargetsLevelPath = "Targets/level_1";
        private const string LevelMapPath = "Level/LevelMap";
        private const string HudPath = "Hud/Hud";
        private const string CameraPath = "Camera/CameraMovement";
        private const string UIPath = "Hud/UICanvas";
        private const string GameFinishPath = "GameFinish";

        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly IInputService _inputService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly IWindowService _windowService;

        private CameraMovement _cameraMovement;
        private Target _target;
        private Logic.StateMachines.Anchor.Anchor _anchor;
        private Entity _destroyObject;
        private Attempts _attempts;
        private GameFinish _gameFinish;

        public List<ISavedProgress> SavedProgressWriters { get; } = new List<ISavedProgress>();
        public List<ISavedProgressReader> SavedProgressReaders { get; } = new List<ISavedProgressReader>();

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticDataService,
            IInputService inputService,IGameStateMachine gameStateMachine,IPersistentProgressService progressService,IWindowService windowService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _windowService = windowService;
            _inputService = inputService;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public void Cleanup()
        {
            SavedProgressWriters.Clear();
            SavedProgressReaders.Clear();
        }

        public void CreateDestroyObject()
        {
          _destroyObject =   InstantiateRegistered(TargetsLevelPath).GetComponent<Entity>();
        }

        public ILevelMap CreateLevelMap()
        {
            return InstantiateRegistered(LevelMapPath).GetComponentInChildren<ILevelMap>();
        }

        public void CreateAnchor()
        {
            AnchorStaticData anchorStaticData = _staticDataService.GetAnchorData();
            _anchor = Object.Instantiate(anchorStaticData.Prefab).GetComponentInChildren<Logic.StateMachines.Anchor.Anchor>();
            _anchor.Construct(_progressService.Progress.AnchorStats.Radius);
            _attempts = _anchor.GetComponentInChildren<Attempts>();
            RegisterProgressWatchers(_anchor.gameObject);
            _anchor.gameObject.SetActive(false);
        }

        public void CreateHud()
        {
           GameObject hud = InstantiateRegistered(HudPath);
           _target = hud.gameObject.GetComponentInChildren<Target>();
           _target.gameObject.SetActive(false);
        }

        public void CreateAICamera()
        {
            GameObject camera = InstantiateRegistered(CameraPath);
            _cameraMovement = camera.GetComponent<CameraMovement>();
        }

        public LevelStateMachine CreateLevelStateMachine()
        {
           return new LevelStateMachine(_target,_cameraMovement,_anchor, _inputService, _destroyObject,_gameFinish);
        }

        public void CreateGameFinish()
        {
            _gameFinish = InstantiateRegistered(GameFinishPath).GetComponent<GameFinish>();
            _gameFinish.Construct(_windowService,_gameStateMachine);
        }

        public void CreateUI()
        {
            var canvas = InstantiateRegistered(UIPath);
            canvas.GetComponentInChildren<ActorUI>().Construct(_attempts);
            canvas.GetComponentInChildren<RestartLevelButton>().Construct(_gameStateMachine);
        }

        private GameObject InstantiateRegistered(string path)
        {
            GameObject gameObject = _assetProvider.Instantiate(path);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (var progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress savedProgressWriters)
                SavedProgressWriters.Add(savedProgressWriters);

            SavedProgressReaders.Add(progressReader);
        }
    }
}