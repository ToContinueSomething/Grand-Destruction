using System;
using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.Services.PersistentProgress;
using Sources.Logic.UI.Window;
using UnityEngine;

namespace Sources.Logic.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _provider;
        private readonly IPersistentProgressService _progressService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider provider, IPersistentProgressService progressService)
        {
            _provider = provider;
            _progressService = progressService;
        }

        public void CreateUIRoot()
        {
            _uiRoot = _provider.Instantiate("UI/UIRoot").transform;
        }

        public TWindow CreateWindow<TWindow>() where TWindow : WindowBase
        {
            if (typeof(TWindow) == typeof(LoseWindow))
            {
                return _provider.Instantiate("UI/LoseWindow", _uiRoot.transform).GetComponent<TWindow>();
            }
            else if (typeof(TWindow) == typeof(WinWindow))
            {
                return _provider.Instantiate("UI/WinWindow", _uiRoot.transform).GetComponent<TWindow>();
            }
            else if (typeof(TWindow) == typeof(ShopWindow))
            {
                return _provider.Instantiate("UI/ShopWindow", _uiRoot.transform).GetComponent<TWindow>();
            }

            throw new InvalidOperationException();
        }
    }
}