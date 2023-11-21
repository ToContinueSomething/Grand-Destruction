using System;

namespace Sources.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public ParameterData ParameterData;
        public LevelMapData LevelMapData;

        public PlayerProgress(int countLevels)
        {
            ParameterData = new ParameterData();
            LevelMapData = new LevelMapData(countLevels);
        }
    }
}