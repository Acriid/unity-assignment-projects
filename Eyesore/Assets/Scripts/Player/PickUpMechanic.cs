using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CircleCollider2D))]
public class PickUpMechanic : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float _detectionRadius = 3f;
    [SerializeField] private LayerMask _itemLayer;

    private List<Item> _itemsInRange = new();
    private Item _targetItem;
    private CircleCollider2D _detectionTrigger;

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        UpdateTargetItem(transform);
    }

    public void Initialize()
    {
        _detectionTrigger = GetComponent<CircleCollider2D>();
        _detectionTrigger.radius = _detectionRadius;
        _detectionTrigger.isTrigger = true;       
    }
    public void UpdateTargetItem(Transform player)
    {
        _itemsInRange.RemoveAll(item => item == null);

        _targetItem = null;

        if(_itemsInRange.Count == 0){return;}

        float bestScore = float.MaxValue;

        foreach(Item item in _itemsInRange)
        {
            float distance = Vector3.Distance(player.position,item.transform.position);

            if(distance < bestScore)
            {
                bestScore = distance;
                _targetItem = item;
            }
        }
        
    }

    public void PickUpItem(Transform itemHolder, bool keepGlobal, Item itemToPickUp)
    {

        itemToPickUp.transform.SetParent(itemHolder,keepGlobal);
        itemToPickUp.transform.SetLocalPositionAndRotation(Vector3.zero, new(0f,0f,0f,0f));
        itemToPickUp.KinematicRigidBody(RigidbodyType2D.Kinematic);
        itemToPickUp.ChangeExcludeLayerMasks(LayerMask.GetMask("Player"));

        if (_itemsInRange.Remove(itemToPickUp) && itemToPickUp == _targetItem)
        {
            _targetItem = null;
        }
        
    }

    public void DropItem(Transform itemHolder, Item itemToPutDown)
    {
        itemToPutDown.transform.SetParent(itemHolder,true);
        itemToPutDown.KinematicRigidBody(RigidbodyType2D.Dynamic);
        itemToPutDown.ChangeExcludeLayerMasks(LayerMask.GetMask("Nothing"));
        _itemsInRange.Add(itemToPutDown);
    }

    

    public Item GetTargetItem()
    {
        return _targetItem;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _itemLayer) == 0) 
            return;

        Item item = collision.GetComponent<Item>();

        if(item != null && !_itemsInRange.Contains(item)) 
            _itemsInRange.Add(item); 
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Item>(out var item))
        {
            _itemsInRange.Remove(item);
            if(_targetItem == item)
                _targetItem = null;
        }       
    }

}
