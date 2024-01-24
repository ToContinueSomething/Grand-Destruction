using System;
using Sources.Logic.UI.Services.Windows;
using Sources.Logic.UI.Window;

namespace Sources.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowBase Template;
        public WindowId WindowId;
    }
}