using System;
using System.Collections.Generic;

namespace Sources.Data
{
    [Serializable]
    public class LevelMapData
    {
        public List<Level> Levels;

        public LevelMapData(int countLevels)
        {
            Levels = new List<Level>(countLevels);

            for (int i = 0; i < Levels.Count; i++) 
                Levels[i] = new Level();
        }

        public int GetCompletedLevelCount()
        {
            for (int i = 0; i < Levels.Count; i++)
            {
                if (Levels[i].isComplete == false)
                {
                    return i;
                    break;
                }
            }

            return Levels.Count - 1;
        }
    }
}