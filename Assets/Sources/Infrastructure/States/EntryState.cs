using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Inform;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Infrastructure.Services.SaveLoad;
using Sources.Infrastructure.Services.StaticData;
using Sources.Services.Input;

namespace Sources.Infrastructure.States
{
    public class EntryState : IState
    {
        private const string NameScene = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public EntryState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(NameScene,EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
          _services.RegisterSingle<IInputService>(new InputService());
          _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
          _services.RegisterSingle<IAssetProvider>(new AssetProvider());
          _services.RegisterSingle<IStaticDataService>(new StaticDataService());
          _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
          _services.RegisterSingle<IInformProgressReaderService>(new InformProgressReaderService(_services.Single<IGameFactory>(),_services.Single<IPersistentProgressService>()));
          _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(),
              _services.Single<IGameFactory>()));
        }
    }
    
}