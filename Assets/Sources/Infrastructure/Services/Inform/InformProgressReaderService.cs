using Sources.Infrastructure.Factory;
using Sources.Infrastructure.Services.PersistentProgress;

namespace Sources.Infrastructure.Services.Inform
{
    class InformProgressReaderService : IInformProgressReaderService
    {
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public InformProgressReaderService(IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
        }
        
        public void Inform()
        {
            foreach (ISavedProgressReader savedProgressReader in _gameFactory.SavedProgressReaders)
            {
                savedProgressReader.LoadProgress(_progressService.Progress);
            }
        }
    }
}