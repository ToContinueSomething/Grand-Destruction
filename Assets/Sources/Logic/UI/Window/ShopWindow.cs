using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Logic.UI.Window
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private Button _continueButton;

        public event Action ContinueClicked;
        
        protected override void Enable()
        {
            _continueButton.onClick.AddListener(OnContinueClicked);
        }

        protected override void Disable()
        {
            _continueButton.onClick.RemoveListener(OnContinueClicked);
        }

        private void OnContinueClicked()
        {
             ContinueClicked?.Invoke();
        }
    }
}