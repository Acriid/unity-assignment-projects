using Unity.VisualScripting;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private GameObject _endCanvas;
    [SerializeField] private Player _player;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _endCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
