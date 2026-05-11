using UnityEngine;

public class ResetSprintObject : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<SprintMechanic>().ResetSprint();
        }
    }
}
