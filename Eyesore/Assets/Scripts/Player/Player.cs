using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _playerSpeed = 10f;
    [SerializeField] private InteractMechanic _pickUpMechanic;
    [SerializeField] private GameObject _itemHolder;
    [SerializeField] private GameObject _worldItemHolder;
    [SerializeField] private Light2D _playerLight;
    [SerializeField] private GameObject _deathCanvas;
    public GameObject EscapeUI;
    private Vector2 _moveInput;


    public bool HoldingItem = false;
    private Item _heldItem;
    

    private Color _normalColor;
    private int health = 3;
    private Coroutine healthReset;

    private float defaultSpeed;
    void Awake()
    {
        _normalColor = _playerLight.color;
        defaultSpeed = _playerSpeed;

        EnableActions();
        SubscribeToFunctions();
    }
    void OnDisable()
    {
        DisableActions();
        UnsubscribeFromFunctions();
    }

    private void EnableActions()
    {
        _inputReader.EnableMoveAction();
        _inputReader.EnablePickUpAction();
        _inputReader.EnableEscapeAction();
        _inputReader.EnableSprintAction();
    }
    private void DisableActions()
    {
        _inputReader.DisableMoveAction();
        _inputReader.DisablePickUpAction();
        _inputReader.DisableEscapeAction();    
        _inputReader.DisableSprintAction();   
    }


    void FixedUpdate()
    {
        MovePlayer(_moveInput);
    }

    public void MovePlayer(Vector2 moveDirection)
    {
        _rigidBody.linearVelocity = moveDirection * _playerSpeed;
    }

    private void SubscribeToFunctions()
    {
        _inputReader.OnMove += OnMove;
        _inputReader.OnInteract += OnInteract;
        _inputReader.OnEscape += OnEscape;
    }
    private void UnsubscribeFromFunctions()
    {
        _inputReader.OnMove -= OnMove;
        _inputReader.OnInteract -= OnInteract;
        _inputReader.OnEscape -= OnEscape;
    }



    private void OnMove(Vector2 newValue)
    {
        _moveInput = newValue;
    }

    private void OnInteract()
    {
        _pickUpMechanic.Interact(this.gameObject);
    }

    public void ForceInteract()
    {
        OnInteract();
    }
    public void DamagePlayer()
    {
        if(healthReset != null)
        {
            StopCoroutine(healthReset);
            healthReset = null;
        }

        StartCoroutine(FlashLight(Color.red));

        health--;

        healthReset = StartCoroutine(ResetPlayerHealth());

        if(health <= 0)
        {
            Time.timeScale = 0;
            if(_deathCanvas == null) return;
            _deathCanvas.SetActive(true);
        }

    }

    public void KillPlayer()
    {
        Time.timeScale = 0;
        if(_deathCanvas == null) return;
        _deathCanvas.SetActive(true);
    }

    public IEnumerator ResetPlayerHealth()
    {
        yield return new WaitForSecondsRealtime(4f);
        health = 3;
    }

    public IEnumerator FlashLight(Color flashColour)
    {
        _playerLight.color = flashColour;

        yield return new WaitForSecondsRealtime(0.5f);
        _playerLight.color = _normalColor;
    }

    private void OnEscape()
    {
        if(EscapeUI == null) return;
        EscapeUI.SetActive(true);
    }

    public void ChangeSpeed(float newSpeed)
    {
        _playerSpeed = newSpeed;
    }
    public float GetSpeed()
    {
        return _playerSpeed;
    }



    public void StopMove()
    {
        _inputReader.DisableMoveAction();
    }

    public void StartMove()
    {
        _inputReader.EnableMoveAction();
    }

    public void StopToggle()
    {
        _inputReader.DisableToggleLightAction();
    }

    public void StartToggle()
    {
        _inputReader.EnableToggleLightAction();
    }

    public GameObject GetLightObject()
    {
        return _playerLight.gameObject;
    }
}
