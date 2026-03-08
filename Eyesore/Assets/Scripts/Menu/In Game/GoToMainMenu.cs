using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
