using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.Inform;
using Sources.Infrastructure.Services.Input;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Infrastructure.Services.SaveLoad;
using Sources.Infrastructure.Services.StaticData;
using Sources.Logic.UI.Services.Factory;
using Sources.Logic.UI.Services.Windows;
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
          _services.RegisterSingle<IGameStateMachine>(_gameStateMachine);
          RegisterStaticDataService();
          
          _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IAssetProvider>(),
              _services.Single<IPersistentProgressService>()));
          
          _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));
          
          _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(),_services.Single<IStaticDataService>(),
              _services.Single<IInputService>(),_services.Single<IGameStateMachine>(),_services.Single<IPersistentProgressService>(),_services.Single<IWindowService>()));
          
          _services.RegisterSingle<IInformProgressReaderService>(new InformProgressReaderService(_services.Single<IGameFactory>(),
              _services.Single<IPersistentProgressService>()));
          
          _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(),
              _services.Single<IGameFactory>()));
        }

        private void RegisterStaticDataService()
        {
            var staticDataService = new StaticDataService();
            staticDataService.Load();
            _services.RegisterSingle<IStaticDataService>(staticDataService);
        }
    }
    
}