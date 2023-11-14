using System;
using UnityEngine;

public class InputRouter : MonoBehaviour
{
    private PlayerInput _input;

    public event Action Clicked;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Player.Click.performed += context => OnClicked();
    }
    

    private void OnClicked()
    {
        Clicked?.Invoke();
    }

    public void Enable()
    {
        _input.Enable();
    }

    public void Disable()
    {
        _input.Disable();
    }
}
