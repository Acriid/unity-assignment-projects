using UnityEngine;


public class DeathCanvas : MonoBehaviour
{
    void OnEnable()
    {
        Time.timeScale = 0;
    }
    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
