using Sources.StaticData;
using UnityEngine;

namespace Sources.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string AnchorPath = "StaticData/Anchor";
        
        private AnchorStaticData _staticData;
        
        public void Load()
        {
            _staticData = Resources.Load<AnchorStaticData>(AnchorPath);
        }
    }
}