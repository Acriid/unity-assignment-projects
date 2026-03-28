using Unity.VisualScripting;
using UnityEngine;

public class TutorialEndTrigger : ActivationTrigger
{
    [SerializeField] private Player _player;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_collisionTag))
        {
            DisableGameObjects();
            _player.ForceInteract();
            EnableGameObjects();
        }
        
    }
    protected override void DisableGameObjects()
    {

        base.DisableGameObjects();
    }
}
