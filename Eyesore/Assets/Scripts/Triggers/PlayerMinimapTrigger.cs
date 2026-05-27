using Unity.VisualScripting;
using UnityEngine;

public class PlayerMinimapTrigger : ActivationTrigger
{
    [SerializeField] private bool _activeMechanic = true;
    [SerializeField] private bool _toggleMechanic = false;
    [SerializeField] private EnemyDirector _enemyDirector;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_collisionTag))
        {
            _enemyDirector.SetShouldGoToMap(_activeMechanic);
            if(_toggleMechanic)
            _activeMechanic = !_activeMechanic;
        }
    }
}
