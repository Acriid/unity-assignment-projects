using UnityEngine;

public class JudgementTrigger : MonoBehaviour
{
    public GameObject _endCanvas;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _endCanvas.GetComponent<EndDialogManager>().ChangeFollow(false);
        }
    }
}
