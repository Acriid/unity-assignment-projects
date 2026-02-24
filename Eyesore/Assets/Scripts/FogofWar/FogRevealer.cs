using UnityEngine;

public class FogRevealer : MonoBehaviour
{
    private FogOfWarManager FogOfWarManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FogOfWarManager.Instance.RegisterVisionSource(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
