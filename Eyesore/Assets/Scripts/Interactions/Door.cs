using Unity.VisualScripting;
using UnityEngine;

public class Door : Interaction
{
    public override void OnInteract(GameObject player)
    {
        this.gameObject.SetActive(false);
    }

    
}
