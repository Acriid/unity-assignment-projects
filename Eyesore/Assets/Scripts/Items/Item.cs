using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Item : Interaction
{
    public ItemSO ItemSO;
    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    [SerializeField] private bool _pickedUp = false;
    [SerializeField] private Transform _itemHolder;
    private Transform _originalParent;

    void OnEnable()
    {
        Initialize();
    }
    private void Initialize()
    {
        if (_rigidBody == null) _rigidBody = GetComponent<Rigidbody2D>();

        _rigidBody.gravityScale = 0;

        if(_collider == null) _collider = GetComponent<Collider2D>();
        
        _originalParent = transform.parent;
    }

    public override void OnInteract(GameObject player)
    {
        if(!CanInteract()) return;

        if(!_pickedUp)
        {
            PickUpItem(player);
        }
        else
        {
            DropItem(player);
        }

    }
    public override string GetPrompt()
    {
        return _pickedUp ? $"Drop {ItemSO.ItemName}" : $"Pick up {ItemSO.ItemName}";
    }
    

    private void PickUpItem(GameObject player)
    {
        // Find item holder on player
        Transform holder = _itemHolder != null ? _itemHolder : player.transform;

        // Parent to holder
        transform.SetParent(holder, true);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        // Make kinematic
        if (_rigidBody != null)
        {
            _rigidBody.bodyType = RigidbodyType2D.Kinematic;
            _rigidBody.linearVelocity = Vector2.zero;
            _rigidBody.angularVelocity = 0f;
        }

        // Change collision layer
        if (_collider != null)
        {
            _collider.excludeLayers = LayerMask.GetMask("Player");
        }

        EnableObjects(true);
        DisableObjects(false);
        _pickedUp = true;
        Debug.Log($"Picked up {ItemSO.ItemName}");
    }
    private void DropItem(GameObject player)
    {
        // Unparent
        transform.SetParent(_originalParent, true);

        // Make dynamic
        if (_rigidBody != null)
        {
            _rigidBody.bodyType = RigidbodyType2D.Dynamic;
            
            // Optional: Add throw velocity based on player movement
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                _rigidBody.linearVelocity = playerRb.linearVelocity;
            }
        }

        // Reset collision layer
        if (_collider != null)
        {
            _collider.excludeLayers = LayerMask.GetMask("Nothing");
        }

        EnableObjects(false);
        DisableObjects(true);
        _pickedUp = false;
        Debug.Log($"Dropped {ItemSO.ItemName}");
    }
    public void KinematicRigidBody(RigidbodyType2D bodyType)
    {
        if (_rigidBody != null)
            _rigidBody.bodyType = bodyType;
    }

    public void ChangeExcludeLayerMasks(LayerMask mask)
    {
        if (_collider != null)
            _collider.excludeLayers = mask;
    }

    public bool IsPickedUp() => _pickedUp;

    
}
