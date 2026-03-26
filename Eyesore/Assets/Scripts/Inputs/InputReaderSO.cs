using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Inputs/InputReader")]
public class InputReaderSO : ScriptableObject
{
    private InputActions _inputActions;
    

    private InputAction _moveAction;

    private InputAction _pickUpAction;

    private InputAction _escapeAction;

    private InputAction _navigateAction;
    private InputAction _submitAction;

    private InputAction _toggleLightAction;

    private InputAction _sprintAction;

    public event Action<Vector2> OnMove;
    public event Action OnPickUp;
    public event Action OnEscape;
    public event Action OnToggleLight;
    public event Action<bool> OnSprint;

    public event Action OnNavigate;
    public event Action OnSubmit;

    


    private Action<InputAction.CallbackContext> movePerformed;
    private Action<InputAction.CallbackContext> moveCanceled;
    private Action<InputAction.CallbackContext> pickUpPerformed;
    private Action<InputAction.CallbackContext> escapePerformed;
    private Action<InputAction.CallbackContext> toggleLightPerformed;
    private Action<InputAction.CallbackContext> sprintStarted;
    private Action<InputAction.CallbackContext> sprintCanceled;

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
        _pickUpAction = _inputActions.Player.Jump;
       _escapeAction = _inputActions.Player.Escape;
       _toggleLightAction = _inputActions.Player.ToggleLight;
       _sprintAction = _inputActions.Player.Sprint;
    }
    private void InitializePlayerEvents()
    {
        movePerformed = ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        moveCanceled = ctx => OnMove?.Invoke(Vector2.zero);

        pickUpPerformed = ctx => OnPickUp?.Invoke();

        escapePerformed = ctx => OnEscape?.Invoke();

        toggleLightPerformed = ctx => OnToggleLight?.Invoke();

        sprintStarted = ctx => OnSprint?.Invoke(true);
        sprintCanceled = ctx => OnSprint?.Invoke(false);
    }

    private void SubscribeActions()
    {
        _moveAction.performed += movePerformed;
        _moveAction.canceled += moveCanceled;

        _pickUpAction.performed += pickUpPerformed;

        _escapeAction.performed += escapePerformed;

        _toggleLightAction.performed += toggleLightPerformed;

        _sprintAction.started += sprintStarted;
        _sprintAction.canceled += sprintCanceled;

        _navigateAction.performed += navigatePerformed;
        _submitAction.performed += submitPerformed;
    }
    private void UnsubscribeActions()
    {
        _moveAction.performed -= movePerformed;
        _moveAction.canceled -= moveCanceled;   

        _pickUpAction.performed -= pickUpPerformed;

        _escapeAction.performed -= escapePerformed;

        _toggleLightAction.performed -= toggleLightPerformed;

        _sprintAction.started -= sprintStarted;
        _sprintAction.canceled -= sprintCanceled;

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

    public void EnablePickUpAction()
    {
        _pickUpAction.Enable();
    }
    public void DisablePickUpAction()
    {
        _pickUpAction.Disable();
    }

    public void EnableEscapeAction()
    {
        _escapeAction.Enable();
    }
    public void DisableEscapeAction()
    {
        _escapeAction.Disable();
    }
    
    public void EnableToggleLightAction()
    {
        _toggleLightAction.Enable();
    }
    public void DisableToggleLightAction()
    {
        _toggleLightAction.Disable();
    }

    public void EnableSprintAction()
    {
        _sprintAction.Enable();
    }
    public void DisableSprintAction()
    {
        _sprintAction.Disable();
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
