using UnityEngine;
using UnityEngine.AI;

public class LightEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navAgent;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Player Light"))
        {
            
        }
    }

    private void ResetEncounter()
    {
        
    }
    public void SetNavAgentDestination(Vector2 destination)
    {
        _navAgent.SetDestination(destination);
    }
}
