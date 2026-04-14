using UnityEngine;

public class PlayerLightManager : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _lightCollider;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _lightCollider.enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _lightCollider.enabled = false;
        }
    }
}
