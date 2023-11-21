using System;

namespace ForestLevelMapMaker.Scripts
{
    public interface ILevelMap
    {
        event Action<int> Selected;
    }
}