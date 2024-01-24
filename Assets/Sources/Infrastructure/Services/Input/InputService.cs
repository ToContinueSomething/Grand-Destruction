using System;
using Sources.Services.Input;

namespace Sources.Infrastructure.Services.Input
{
    public class InputService : IInputService
    {
        private readonly PlayerInput _input;

        public event Action Clicked;

        public InputService()
        {
            _input = new PlayerInput();
        }

        public void Enable()
        {
            _input.Enable();
            _input.Player.Click.performed += context => OnClicked();
        }

        public void Disable()
        {
            _input.Disable();
            _input.Player.Click.performed -= context => OnClicked();
        }

        private void OnClicked()
        {
            Clicked?.Invoke();
        }
    }
}