using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public InputReaderSO InputReaderSO;

    void OnEnable()
    {
        InputReaderSO.OnResetScene += OnSceneReset;
    }
    void OnDisable()
    {
        InputReaderSO.OnResetScene -= OnSceneReset;
    }
    private void OnSceneReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
