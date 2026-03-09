using Unity.VisualScripting;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] public GameObject _endCanvas;
    [SerializeField] private Player _player;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _endCanvas.SetActive(true);
        }
    }
    
    public void ChangeFollow(bool newValue)
    {
        _endCanvas.GetComponentInChildren<EndDialogManager>().ChangeFollow(newValue);
    }
}
