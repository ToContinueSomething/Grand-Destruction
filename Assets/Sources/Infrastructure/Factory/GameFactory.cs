using System.Collections.Generic;
using ForestLevelMapMaker.Scripts;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Sources.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private const string TargetsLevelPath = "Targets/level_1";
        private const string LevelMapPath = "Level/LevelMap";

        public List<ISavedProgress> SavedProgressWriters { get; } = new List<ISavedProgress>();
        public List<ISavedProgressReader> SavedProgressReaders { get; } = new List<ISavedProgressReader>();

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void CreateTargetDestroy()
        {
            InstantiateRegistered(TargetsLevelPath);
        }


        public void Cleanup()
        {
            SavedProgressWriters.Clear();
            SavedProgressReaders.Clear();
        }

        public ILevelMap CreateLevelMap()
        {
            return InstantiateRegistered(LevelMapPath).GetComponentInChildren<ILevelMap>();
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