using UnityEngine;

public class KeyExit : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("ExitKey"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
