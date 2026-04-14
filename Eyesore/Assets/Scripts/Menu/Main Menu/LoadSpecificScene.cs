using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    [SerializeField] private int _sceneNumber;
    public void OnClick()
    {
        SceneManager.LoadScene(_sceneNumber);
    }
}
