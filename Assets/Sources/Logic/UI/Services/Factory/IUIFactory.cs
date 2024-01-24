using Sources.Infrastructure.Services;
using Sources.Logic.UI.Window;

namespace Sources.Logic.UI.Services.Factory
{
    public interface IUIFactory :  IService
    {
        void CreateUIRoot();
        TWindow CreateWindow<TWindow>() where TWindow : WindowBase;
    }
}