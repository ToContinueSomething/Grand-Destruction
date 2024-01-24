using System;

namespace Sources.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public Stats AnchorStats;
        public LevelMapData LevelMapData;

        public PlayerProgress(int countLevels)
        {
            AnchorStats = new Stats();
            LevelMapData = new LevelMapData(countLevels);
        }
    }
}