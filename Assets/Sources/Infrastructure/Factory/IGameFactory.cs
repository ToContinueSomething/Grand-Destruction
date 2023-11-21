using System.Collections.Generic;
using ForestLevelMapMaker.Scripts;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Services;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        void CreateTargetDestroy();
        List<ISavedProgress> SavedProgressWriters { get; }
        List<ISavedProgressReader> SavedProgressReaders { get; }
        void Cleanup();
        ILevelMap CreateLevelMap();
    }
}