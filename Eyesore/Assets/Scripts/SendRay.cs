using UnityEngine;

public class SendRay : MonoBehaviour
{
    public GameObject player;
    public LayerMask layerMask;
    void Update()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(player.transform.position,Vector2.right,100f,layerMask);
        if(raycastHit2D)
        {
            Debug.Log(raycastHit2D.point);
        }
    }
}
