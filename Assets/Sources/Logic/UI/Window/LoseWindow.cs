using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Logic.UI.Window
{
    public class LoseWindow : ResultWindow
    {
        [SerializeField] private Button _restartButton;

        public event Action RestartButtonClicked;

        protected override void Enable()
        {
            base.Enable();
            _restartButton.onClick.AddListener(OnRestartClicked);
        }

        protected override void Disable()
        {
            base.Disable();
            _restartButton.onClick.RemoveListener(OnRestartClicked);
        }

        private void OnRestartClicked()
        {
            RestartButtonClicked?.Invoke();        
        }
    }
}