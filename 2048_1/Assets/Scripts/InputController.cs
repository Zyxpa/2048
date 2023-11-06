using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] double MaxTime;
    public SwipeAction swipeAction;
    
    GameInput inputActions;

    Vector2 startPosition;
    double startTime;

    void Awake()
    {
        inputActions = new GameInput();
    }
    void Start()
    {
        inputActions.Main.PrimaryContact.started += ctx => StarContact(ctx);
        inputActions.Main.PrimaryContact.canceled += ctx => EndContact(ctx);
    }

    private void StarContact(InputAction.CallbackContext ctx)
    {
        startPosition = inputActions.Main.PrimaryPosition.ReadValue<Vector2>();
        startTime = ctx.time;
    }
    private void EndContact(InputAction.CallbackContext ctx)
    {
        if (ctx.time - startTime > MaxTime)
            return;
        Vector2 swipe = inputActions.Main.PrimaryPosition.ReadValue<Vector2>() - startPosition;

        if (Vector2.Dot(swipe, Vector2.up) > Vector2.Dot(swipe, Vector2.down) && Vector2.Dot(swipe, Vector2.up) > Vector2.Dot(swipe, Vector2.right) && Vector2.Dot(swipe, Vector2.up) > Vector2.Dot(swipe, Vector2.left))
            swipeAction.Invoke(Vector2.up);
        if (Vector2.Dot(swipe, Vector2.down) > Vector2.Dot(swipe, Vector2.right) && Vector2.Dot(swipe, Vector2.down) > Vector2.Dot(swipe, Vector2.left))
            swipeAction.Invoke(Vector2.down);
        if (Vector2.Dot(swipe, Vector2.right)  > Vector2.Dot(swipe, Vector2.left))
            swipeAction.Invoke(Vector2.right);
        swipeAction.Invoke(Vector2.left);
    }
    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    public class SwipeAction : UnityEvent<Vector2>
    {
    }
}
