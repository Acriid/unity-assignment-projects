using UnityEngine;

public class KeyExit : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<Player>().HoldingItem)
            {  
                this.gameObject.SetActive(false);
            }
        }
    }
}
