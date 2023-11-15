using System;
using UnityEngine;

public class InputRouter : MonoBehaviour
{
    private PlayerInput _input;

    public event Action Clicked;
    
    
    private void OnClicked()
    {
        Clicked?.Invoke();
    }

    public void Enable()
    {
        _input = new PlayerInput();
        _input.Enable();
        _input.Player.Click.performed += context => OnClicked();
    }

    public void Disable()
    {
        _input.Disable();
    }
}
