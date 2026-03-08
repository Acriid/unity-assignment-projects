using UnityEngine;

public class Resume : MonoBehaviour
{
    public GameObject PauseCanvas;
    public void OnClick()
    {
        PauseCanvas.SetActive(false);
    }
}
