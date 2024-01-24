using Sources.StaticData;
using Sources.StaticData.Windows;
using UnityEngine;

namespace Sources.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string AnchorPath = "StaticData/Anchor/Anchor";
        private const string WindowPath = "StaticData/Window/WindowStaticData";

        private AnchorStaticData _anchorStaticData;
        private WindowStaticData _windowStaticData;

        public void Load()
        {
            _windowStaticData = Resources.Load<WindowStaticData>(WindowPath);
            _anchorStaticData = Resources.Load<AnchorStaticData>(AnchorPath);
        }

        public AnchorStaticData GetAnchorData() => _anchorStaticData;

        public WindowStaticData GetWindowData() => _windowStaticData;
    }
}