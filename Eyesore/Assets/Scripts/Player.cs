using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _playerSpeed;
    private Vector2 _moveInput;


    void Awake()
    {
        _inputReader.EnableMoveAction();
        SubscribeToFunctions();
    }
    void OnDisable()
    {
        _inputReader.DisableMoveAction();
        UnsubscribeFromFunctions();
    }


    void Update()
    {
        
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
    }
    private void UnsubscribeFromFunctions()
    {
        _inputReader.OnMove -= OnMove;
    }



    private void OnMove(Vector2 newValue)
    {
        _moveInput = newValue;
    }
}
