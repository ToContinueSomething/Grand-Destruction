using System.Collections.Generic;
using ForestLevelMapMaker.Scripts;
using Sources.Infrastructure.Services;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic.StateMachines;
using Sources.Services;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgress> SavedProgressWriters { get; }
        List<ISavedProgressReader> SavedProgressReaders { get; }
        void CreateDestroyObject();
        void Cleanup();
        ILevelMap CreateLevelMap();
        void CreateAnchor();
        void CreateHud();
        void CreateAICamera();
        LevelStateMachine CreateLevelStateMachine();
        void CreateUI();
        void CreateGameFinish();
    }
}