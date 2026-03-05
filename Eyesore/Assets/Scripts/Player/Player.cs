using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private PickUpMechanic _pickUpMechanic;
    [SerializeField] private GameObject _itemHolder;
    [SerializeField] private GameObject _worldItemHolder;
    private Vector2 _moveInput;
    private bool _holdingItem = false;
    private Item _heldItem;

    void Awake()
    {
        _inputReader.EnableMoveAction();
        _inputReader.EnablePickUpAction();
        SubscribeToFunctions();
    }
    void OnDisable()
    {
        _inputReader.DisableMoveAction();
        _inputReader.DisablePickUpAction();
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
        _inputReader.OnPickUp += OnPickUp;
    }
    private void UnsubscribeFromFunctions()
    {
        _inputReader.OnMove -= OnMove;
        _inputReader.OnPickUp -= OnPickUp;
    }



    private void OnMove(Vector2 newValue)
    {
        _moveInput = newValue;
    }

    private void OnPickUp()
    {
        if(!_holdingItem)
        {
            
            _heldItem = _pickUpMechanic.GetTargetItem();
            if(_heldItem == null) return;

            if(_heldItem.gameObject.TryGetComponent(out DialogTrigger trigger))
            {
                trigger.TriggerDialog();
            }

            _pickUpMechanic.PickUpItem(_itemHolder.transform,false,_heldItem);
            _holdingItem = true;
        }
        else
        {
            _pickUpMechanic.DropItem(_worldItemHolder.transform,_heldItem);
            _heldItem = null;
            _holdingItem = false;
        }
    }
}
