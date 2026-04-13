using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GoalTrigger : GoalObject
{
    public GameObject GoalItem;
    private Vector2 _keyPosition;
    private Item _itemComponent;
    public Player Player;
    void Awake()
    {
        _keyPosition = this.transform.position;
        if(GoalSO != null)
        {
            GoalSO.SetGoalPosition(this.transform.position);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_collisionTag) && collision.gameObject == GoalItem)
        {
            Player.ForceInteract();
            GoalItem.GetComponent<Rigidbody2D>().linearVelocity = new(0,0);
            GoalItem.GetComponent<Collider2D>().enabled = false;
            GoalItem.GetComponent<Light2D>().color = Color.blueViolet;
            GoalItem.transform.SetParent(this.transform,true);
            if(GoalItem.TryGetComponent<Item>(out _itemComponent))
            {
                _itemComponent.SetInteractable(false);
            }
            GoalSO.GoalComplete = true;
        }
    }
}
