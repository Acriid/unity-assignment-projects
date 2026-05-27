using Unity.VisualScripting;
using UnityEngine;

public class EnemyTrigger : ActivationTrigger
{
    [SerializeField] private EnemyDirector _enemyDirector;
    [SerializeField] private GameObject _forceObject;
    [SerializeField] private Vector2 _forcePosition;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(_enemyDirector == null) return;
        if(collision.CompareTag(_collisionTag))
        {
            if(_forceObject != null)
            {
                _enemyDirector.ForceEnemyMove(_forceObject.transform.position);
            }
            else
            {
                _enemyDirector.ForceEnemyMove(_forcePosition);
            }
        }
    }
}
