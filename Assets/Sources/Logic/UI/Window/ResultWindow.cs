using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Logic.UI.Window
{
    public class ResultWindow : WindowBase
    {
        [SerializeField] private Button _mapButton;
        [SerializeField] private Button _shopButton;

        public event Action<ResultWindow> ShopClicked;
        public event Action<ResultWindow> MapClicked;

        protected override void Enable()
        {
            _mapButton.onClick.AddListener(OnMapClicked);
            _shopButton.onClick.AddListener(OnShopClicked);
        }

        protected override void Disable()
        {
            _mapButton.onClick.RemoveListener(OnMapClicked);
            _shopButton.onClick.RemoveListener(OnShopClicked);
        }

        private void OnMapClicked()
        {
            MapClicked?.Invoke(this);
        }

        private void OnShopClicked()
        {
            ShopClicked?.Invoke(this);
        }

    }
}