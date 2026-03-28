using Unity.VisualScripting;
using UnityEngine;

public class ItemTrigger : ActivationTrigger
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _item;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_collisionTag) && collision.gameObject == _item)
        {

            EnableGameObjects();
            DisableGameObjects();
            _player.ForceInteract();
        }
        else
        {
            ShowUI($"Get {_item.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideUI();
    }
}
