using UnityEngine;

public class TestTimerStart : MonoBehaviour
{
    public float Timer;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        Timer = 0f;
    }
    void Update()
    {
        Timer += Time.deltaTime;
    }
}
