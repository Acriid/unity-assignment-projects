using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Inputs/InputReader")]
public class InputReaderSO : ScriptableObject
{
    private InputActions _inputActions;
    

    private InputAction _moveAction;

    private InputAction _interactAction;

    private InputAction _escapeAction;

    private InputAction _navigateAction;
    private InputAction _submitAction;

    private InputAction _toggleLightAction;

    private InputAction _sprintAction;

    //Devtools
    private InputAction _showEnemyAction;
    private InputAction _resetSceneAction;
    private InputAction _showEnemyStatsAction;
    private InputAction _godModePlayerAction;

    //Events
    public event Action<Vector2> OnMove;
    public event Action OnInteract;
    public event Action OnEscape;
    public event Action OnToggleLight;
    public event Action<bool> OnSprint;

    public event Action OnNavigate;
    public event Action OnSubmit;

    //Devtools
    public event Action OnShowEnemyAction;
    public event Action OnResetScene;
    public event Action OnShowEnemyStats;
    public event Action OnGodModePlayer;

    //Actions
    private Action<InputAction.CallbackContext> movePerformed;
    private Action<InputAction.CallbackContext> moveCanceled;
    private Action<InputAction.CallbackContext> interactPerformed;
    private Action<InputAction.CallbackContext> escapePerformed;
    private Action<InputAction.CallbackContext> toggleLightPerformed;
    private Action<InputAction.CallbackContext> sprintStarted;
    private Action<InputAction.CallbackContext> sprintCanceled;

    private Action<InputAction.CallbackContext> navigatePerformed;
    private Action<InputAction.CallbackContext> submitPerformed;

    //Devtools
    private Action<InputAction.CallbackContext> showEnemyPerformed;
    private Action<InputAction.CallbackContext> resetScenePerformed;
    private Action<InputAction.CallbackContext> showEnemyStatsPerformed;
    private Action<InputAction.CallbackContext> godModePlayerPerformed;


    void OnEnable()
    {
        _inputActions = new();

        InitializePlayerActions();
        InitializePlayerEvents();

        InitializeUIActions();
        InitializeUIEvents();

        SubscribeActions();


        //Devtools
        InitializeDevtools();
        InitializeDevtoolEvents();
        SubscribeDevtools();
    }
    void OnDisable()
    {
        UnsubscribeActions();

        //Devtools
        UnsubscribeDevtools();
    }


    private void InitializePlayerActions()
    {
        _moveAction = _inputActions.Player.Move;
        _interactAction = _inputActions.Player.Jump;
       _escapeAction = _inputActions.Player.Escape;
       _toggleLightAction = _inputActions.Player.ToggleLight;
       _sprintAction = _inputActions.Player.Sprint;
    }
    private void InitializePlayerEvents()
    {
        movePerformed = ctx => OnMove?.Invoke(ctx.ReadValue<Vector2>());
        moveCanceled = ctx => OnMove?.Invoke(Vector2.zero);

        interactPerformed = ctx => OnInteract?.Invoke();

        escapePerformed = ctx => OnEscape?.Invoke();

        toggleLightPerformed = ctx => OnToggleLight?.Invoke();

        sprintStarted = ctx => OnSprint?.Invoke(true);
        sprintCanceled = ctx => OnSprint?.Invoke(false);
    }

    private void SubscribeActions()
    {
        _moveAction.performed += movePerformed;
        _moveAction.canceled += moveCanceled;

        _interactAction.performed += interactPerformed;

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

        _interactAction.performed -= interactPerformed;

        _escapeAction.performed -= escapePerformed;

        _toggleLightAction.performed -= toggleLightPerformed;

        _sprintAction.started -= sprintStarted;
        _sprintAction.canceled -= sprintCanceled;

        _navigateAction.performed -= navigatePerformed;
        _submitAction.performed -= submitPerformed;             
    }

    private void InitializeDevtools()
    {
        _showEnemyAction = _inputActions.DevTools.ShowEnemy;
        _showEnemyStatsAction = _inputActions.DevTools.ShowEnemyStats;
        _resetSceneAction = _inputActions.DevTools.ResetScene;
        _godModePlayerAction = _inputActions.DevTools.PlayerGodmode;
    }
    private void InitializeDevtoolEvents()
    {
        showEnemyPerformed = ctx => OnShowEnemyAction?.Invoke();
        showEnemyStatsPerformed = ctx => OnShowEnemyStats?.Invoke();
        resetScenePerformed = ctx => OnResetScene?.Invoke();
        godModePlayerPerformed = ctx => OnGodModePlayer?.Invoke();
    }
    private void SubscribeDevtools()
    {
        _showEnemyAction.performed += showEnemyPerformed;
        _showEnemyStatsAction.performed += showEnemyStatsPerformed;
        _resetSceneAction.performed += resetScenePerformed;
        _godModePlayerAction.performed += godModePlayerPerformed;
    }
    private void UnsubscribeDevtools()
    {
        _showEnemyAction.performed -= showEnemyPerformed;
        _showEnemyStatsAction.performed -= showEnemyStatsPerformed;
        _resetSceneAction.performed -= resetScenePerformed; 
        _godModePlayerAction.performed -= godModePlayerPerformed;      
    }
    public void EnableDevtools()
    {
        _showEnemyAction.Enable();
        _showEnemyStatsAction.Enable();
        _resetSceneAction.Enable();
        _godModePlayerAction.Enable();
    }
    public void DisableDevtools()
    {
        _showEnemyAction.Disable();
        _showEnemyStatsAction.Disable();
        _resetSceneAction.Disable(); 
        _godModePlayerAction.Disable();      
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
        _interactAction.Enable();
    }
    public void DisablePickUpAction()
    {
        _interactAction.Disable();
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
