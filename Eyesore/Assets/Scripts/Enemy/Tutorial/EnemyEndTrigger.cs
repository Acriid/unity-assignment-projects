using UnityEngine;

public class EnemyEndTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
