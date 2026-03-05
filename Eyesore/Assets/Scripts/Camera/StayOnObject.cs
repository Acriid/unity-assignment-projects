using UnityEngine;

public class StayOnObject : MonoBehaviour
{
    public GameObject FollowObject;
    void LateUpdate()
    {
        this.transform.position = FollowObject.transform.position;
    }
}
