using Unity.VisualScripting;
using UnityEngine;

public class ItemTrigger : ActivationTrigger
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _item;
    [SerializeField] private string _triggerText;
    [SerializeField] private bool _forceInteract = true;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_collisionTag) && collision.gameObject == _item)
        {

            EnableGameObjects();
            DisableGameObjects();
            if (_player != null && _forceInteract)
                _player.ForceInteract();

        }
        else if(collision.TryGetComponent<Player>(out Player player))
        {
            if(player.HoldingItem != true)
            {
                ShowUI($"Get {_item.name}");  
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideUI();
    }
}
