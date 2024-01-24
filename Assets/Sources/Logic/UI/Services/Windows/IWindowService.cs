using Sources.Infrastructure.Services;
using Sources.Logic.UI.Window;

namespace Sources.Logic.UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);
        TWindow Create<TWindow>() where TWindow : WindowBase;
    }
}