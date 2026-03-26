using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Vector2 _teleportPosition;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.position = _teleportPosition;
        }
    }

    public void ChangeTeleportPosition(Vector2 newPosition)
    {
        _teleportPosition = newPosition;
    }
}
