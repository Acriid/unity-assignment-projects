using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Inputs/InputReader")]
public class InputReaderSO : ScriptableObject
{
    private InputActions _inputActions;

    private InputAction _moveAction;

    public event Action<Vector2> OnMove;

    private Action<InputAction.CallbackContext> movePerformed;
    private Action<InputAction.CallbackContext> moveCancled;

    void OnEnable()
    {
        _inputActions = new();

        InitializePlayerActions();
        InitializePlayerEvents();

        SubscribeActions();
    }
    void OnDisable()
    {
        UnsubscribeActions();
    }


    private void InitializePlayerActions()
    {
        _moveAction = _inputActions.Player.Move;
    }
    private void InitializePlayerEvents()
    {
        movePerformed = ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        moveCancled = ctx => OnMove?.Invoke(Vector2.zero);
    }

    private void SubscribeActions()
    {
        _moveAction.performed += movePerformed;
        _moveAction.canceled += moveCancled;
    }
    private void UnsubscribeActions()
    {
        _moveAction.performed -= movePerformed;
        _moveAction.canceled -= moveCancled;        
    }

    public void EnableMoveAction()
    {
        _moveAction.Enable();
    }
    public void DisableMoveAction()
    {
        _moveAction.Disable();
    }
}
