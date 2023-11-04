using UnityEngine;
using UnityEngine.Events;

public class InputController 
{
    private GameInput inputActions;
    public InputHandler InputEvent = new InputHandler();

    public InputController()
    {
        inputActions = new GameInput();
        inputActions.Enable();
    }

    public void Update()
    {
        var inputDirecton = inputActions.Main.Keyboard.ReadValue<Vector2>();
        if (inputDirecton != Vector2.zero)
            InputEvent?.Invoke(inputDirecton);
    }

    public class InputHandler : UnityEvent<Vector2>
    {
    }
}
