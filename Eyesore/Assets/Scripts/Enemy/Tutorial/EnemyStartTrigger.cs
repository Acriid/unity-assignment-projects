using Unity.VisualScripting;
using UnityEngine;

public class EnemyStartTrigger : MonoBehaviour
{
    [SerializeField] private LightEnemy _enemy;
    [SerializeField] private Vector2 _destination;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _enemy.SetNavAgentDestination(_destination);
            this.gameObject.SetActive(false);
        }
    }
}
