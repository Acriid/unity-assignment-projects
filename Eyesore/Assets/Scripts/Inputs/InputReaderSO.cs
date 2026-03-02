using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Inputs/InputReader")]
public class InputReaderSO : ScriptableObject
{
    private InputActions _inputActions;
    

    private InputAction _moveAction;

    private InputAction _navigateAction;
    private InputAction _submitAction;

    public event Action<Vector2> OnMove;


    public event Action OnNavigate;
    public event Action OnSubmit;


    private Action<InputAction.CallbackContext> movePerformed;
    private Action<InputAction.CallbackContext> moveCancled;

    private Action<InputAction.CallbackContext> navigatePerformed;
    private Action<InputAction.CallbackContext> submitPerformed;

    void OnEnable()
    {
        _inputActions = new();

        InitializePlayerActions();
        InitializePlayerEvents();

        InitializeUIActions();
        InitializeUIEvents();

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

        _navigateAction.performed += navigatePerformed;
        _submitAction.performed += submitPerformed;
    }
    private void UnsubscribeActions()
    {
        _moveAction.performed -= movePerformed;
        _moveAction.canceled -= moveCancled;   

        _navigateAction.performed -= navigatePerformed;
        _submitAction.performed -= submitPerformed;             
    }

    public void EnableMoveAction()
    {
        _moveAction.Enable();
    }
    public void DisableMoveAction()
    {
        _moveAction.Disable();
    }

    private void InitializeUIEvents()
    {
        navigatePerformed = ctx => OnNavigate?.Invoke();
        submitPerformed = ctx => OnSubmit?.Invoke();       
    }

    private void InitializeUIActions()
    {
        _navigateAction = _inputActions.UI.Navigate;
        _submitAction = _inputActions.UI.Submit;       
    }

    public void EnableNavigateAction()
    {
        _navigateAction.Enable();
    }
    public void DisableNavigateAction()
    {
        _navigateAction.Disable();
    }
    public void EnableSubmitAction()
    {
        _submitAction.Enable();
    }
    public void DisableSubmitAction()
    {
        _submitAction.Disable();
    }
}
