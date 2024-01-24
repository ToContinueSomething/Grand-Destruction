using Sources.Logic.UI.Services.Factory;
using Sources.Logic.UI.Window;

namespace Sources.Logic.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        private ShopWindow _shopWindow;
        private WinWindow _winWindow;
        private LoseWindow _loseWindow;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.None:
                    break;
                case WindowId.Shop:
                    _shopWindow.Show();
                    break;
                case WindowId.Win:
                    _winWindow.Show();
                    break;
                case WindowId.Lose:
                    _loseWindow.Show();
                    break;
            }
        }
        
        public TWindow Create<TWindow>() where TWindow : WindowBase
        {
            return _uiFactory.CreateWindow<TWindow>();
        }
    }
}