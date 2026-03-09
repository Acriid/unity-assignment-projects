using UnityEngine;

public class JudgementTrigger : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject EndTrigger;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            mainCamera.transform.position = new Vector3(-92f,-30f,-1f);
            EndTrigger.transform.position = new Vector3(-110.5f,-30.5f,0);
            EndTrigger.GetComponent<GameEnd>()._endCanvas
            .GetComponentInChildren<EndDialogManager>().ChangeFollow(false);
        }
    }
}
