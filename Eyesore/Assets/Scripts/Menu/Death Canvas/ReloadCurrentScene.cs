using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadCurrentScene : MonoBehaviour
{
    public void RelodeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
