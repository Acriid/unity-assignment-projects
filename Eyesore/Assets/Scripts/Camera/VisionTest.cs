using UnityEngine;

public class VisionTest : MonoBehaviour
{
    public RenderTexture visionRT;
    public GameObject FogOfWar;
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(10,10,256,256), visionRT);
    }
    void Start()
    {
        FogOfWar.GetComponent<MeshRenderer>().sortingOrder = 10;
    }
}
