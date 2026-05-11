using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private Vector2 _teleportPosition;
    [SerializeField] private GameObject _teleportObject;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(_teleportObject != null)
            {
                collision.transform.position = _teleportObject.transform.position;
            }
            else
            {
                collision.transform.position = _teleportPosition;
            }
            
        }
    }

    public void ChangeTeleportPosition(Vector2 newPosition)
    {
        _teleportPosition = newPosition;
    }
}
