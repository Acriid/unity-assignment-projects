using UnityEngine;

public class TestTimerEnd : MonoBehaviour
{
    public TestTimerStart testTimerStart;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        Debug.Log(testTimerStart.Timer);
    }
}
