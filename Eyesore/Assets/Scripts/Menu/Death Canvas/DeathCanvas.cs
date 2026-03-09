using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCanvas : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(BackToMenu());
    }
    private IEnumerator BackToMenu()
    {
        yield return new WaitForSecondsRealtime(7f);
        SceneManager.LoadScene("Main Menu");
    }
}
