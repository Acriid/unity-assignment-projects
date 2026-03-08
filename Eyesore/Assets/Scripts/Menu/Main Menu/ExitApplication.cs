using UnityEngine;

public class ExitApplication : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("Application quit");
        Application.Quit();
    }
}