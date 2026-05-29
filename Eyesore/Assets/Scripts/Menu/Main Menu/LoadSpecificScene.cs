using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    [SerializeField] private int _sceneNumber;
    public void OnClick()
    {
        SceneManager.LoadScene(_sceneNumber);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(_sceneNumber);
        }
    }
}
