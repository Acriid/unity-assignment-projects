using Unity.VisualScripting;
using UnityEngine;

public class EnemyCheckpointTrigger : MonoBehaviour
{
    [SerializeField] private LightEnemy _enemy;
    [SerializeField] private Vector2 _destination;
    [SerializeField] private GameObject _nextDestination;
    [SerializeField] private string _collision;
    public bool RemoveObject = true;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_collision))
        {
            if(_nextDestination == null)
            {
                _enemy.SetNavAgentDestination(_destination);  
            }
            else
            {
                _enemy.SetNavAgentDestination(_nextDestination.transform.position);
            }
            if(RemoveObject)
            this.gameObject.SetActive(false);
        }
    }
}
