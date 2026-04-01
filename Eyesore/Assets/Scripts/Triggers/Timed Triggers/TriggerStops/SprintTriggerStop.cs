using UnityEngine;

public class SprintTriggerStop : MonoBehaviour
{
    [SerializeField] private TimedTrigger _timedTrigger;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        _timedTrigger.StopTimer();
    }
}
